using UnityEngine;

namespace NPLTV.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _transform;

        private void Awake() 
        {
            _animator = GetComponent<Animator>(); 
            _transform = transform;   
        }

        public void TurnRight() 
        {
            _transform.localScale = Vector3.one;
            SetTrigger("Turn Right");
        }

        public void TurnLeft()
        {
            _transform.localScale = new Vector3(-1, 1, 1);
            SetTrigger("Turn Right");
        }

        public void SetVelocity(float velocity)
        {
            _animator.SetFloat("Velocity", velocity);
        }

        public void SetGrounded(bool grounded)
        {
            _animator.SetBool("Grounded", grounded);
        }

        public void SetTrigger(string name)
        {
            _animator.SetTrigger(name);
        }

        public void SetAction(string name, float speed)
        {
            _animator.SetFloat("Action Speed", speed);
            _animator.Play(name);
        }
    }
}