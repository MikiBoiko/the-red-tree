using UnityEngine;

namespace NPLTV.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Motor : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] protected Rigidbody2D physics;

        private void Awake()
        {
            physics = GetComponent<Rigidbody2D>();
        }

        public virtual void AddForce(Vector2 force) => physics.AddForce(force);
        public void AddInstantForce(Vector2 force)
        {
            physics.velocity = Vector2.zero;
            AddForce(force);
        }

        public Vector2 GetPosition() => physics.position;

        public void SetWeight(float weight) => physics.mass = weight / 100;
    }
}