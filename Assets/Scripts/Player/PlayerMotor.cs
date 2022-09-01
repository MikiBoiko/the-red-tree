using UnityEngine;
using NPLTV.Movement;

namespace NPLTV.Player
{
    public class PlayerMotor : Motor
    {
        [SerializeField] private PlayerAnimator _animator;

        [Header("Movement")]
        public float speed;
        public float acceleratingForce;
        public float jumpForce;
        public float fastFall;
        [SerializeField] private float _movingDirection;
        [field: SerializeField] public int Direction { private set; get; }

        [Header("Grounded")]
        [Range(0f, 1f)]
        [SerializeField] private float _groundedVelocityDecay;
        [SerializeField] private float _legGap = 1f;
        [SerializeField] private LayerMask _groundLayerMask;
        [field: SerializeField] public bool Grounded { private set; get; }
        
        public delegate void PlayerMotorEvent();
        public PlayerMotorEvent OnGrounded, OnAir; 

        #region Initialization
        private void Awake()
        {
            _animator = GetComponent<PlayerAnimator>();

            OnGrounded += () =>
            {
                _animator.SetGrounded(true);
            };

            OnAir += () =>
            {
                _animator.SetGrounded(false);
            };
        }

        private void Start()
        {
            Reset();
        }

        private void Reset()
        {
            RotateRight();
        }

        private void FixedUpdate()
        {
            Grounded = CheckGrounded();

            Move(_movingDirection);
        }
        #endregion

        #region Motor
        // FIXME ? : Maybe there is a better way for the animation events to get parameters...
        public void AddForce(string value)
        {
            string[] temp = value.Split(',');
            base.AddForce(new Vector2(
                float.Parse(temp[0]) * Direction,
                float.Parse(temp[1])
            ));
        }
        #endregion

        #region Movement
        private void RotateLeft()
        {
            _animator.TurnLeft();
            Direction = -1;
        }

        private void RotateRight()
        {
            _animator.TurnRight();
            Direction = 1;
        }

        public void SetMovingDirection(Vector2 direction)
        {
            _movingDirection = direction.x;

            // Fast fall
            physics.gravityScale = (direction.y < 0) ? 
            1 - (direction.y * fastFall)
            :
            1;
        }

        public void Move(float x)
        {
            physics.AddRelativeForce(Vector2.right * acceleratingForce * x);
            if(Mathf.Abs(physics.velocity.x) > speed) 
                physics.velocity = new Vector2(Mathf.Sign(physics.velocity.x) * speed, physics.velocity.y);

            _animator.SetVelocity(Mathf.Abs(physics.velocity.x));
            if (x > 0) 
            {
                RotateRight();
            }
            else if (x < 0) 
            {
                RotateLeft();
            }
            if (_movingDirection == 0f && Grounded)
                physics.velocity = new Vector2(physics.velocity.x * _groundedVelocityDecay, physics.velocity.y);
        }
        #endregion

        #region Jumping
        public void Jump()
        {
            physics.velocity = new Vector2(physics.velocity.x, 0);
            AddForce(Vector2.up * jumpForce);
        }
        #endregion

        #region Grounded
        private bool RaycastCheckGrounded(RaycastHit2D hit)
        {
            if (hit.collider && hit.distance < 0.1f)
            {
                if(!Grounded)
                    OnGrounded.Invoke();
                return true;
            }
            if (Grounded)
                OnAir.Invoke();
            return false;
        }

        private bool CheckGrounded()
        {
            RaycastHit2D _hitL = Physics2D.Raycast(physics.position - Vector2.right * (_legGap / 2), Vector2.down, 10f, _groundLayerMask);
            RaycastHit2D _hitR = Physics2D.Raycast(physics.position + Vector2.right * (_legGap / 2), Vector2.down, 10f, _groundLayerMask);

            return (RaycastCheckGrounded(_hitL) || RaycastCheckGrounded(_hitR)) && physics.velocity.y == 0f;
        }
        #endregion

        #region Editor
        private void OnDrawGizmos()
        {
            if (Grounded) Gizmos.color = Color.green;
            else Gizmos.color = Color.white;

            Gizmos.DrawLine(physics.position - Vector2.right * (_legGap / 2), physics.position - Vector2.right * (_legGap / 2) + Vector2.down);
            Gizmos.DrawLine(physics.position + Vector2.right * (_legGap / 2), physics.position + Vector2.right * (_legGap / 2) + Vector2.down);
        }
        #endregion
    }
}