using UnityEditor;
using UnityEngine;

namespace NPLTV 
{
    public abstract class CameraController : MonoBehaviour
    {
        protected static CameraController instance;

        [SerializeField] protected new Camera camera;
        [SerializeField] protected new Transform transform;

        public static Vector2 Position => instance.transform.position;

        public static Vector2 Center => instance.center;
        [SerializeField] protected Vector2 center;

        public static Vector2 Size => instance.size;
        [SerializeField] protected Vector2 size;

        [SerializeField] protected Vector2 offset, extraOffset;

        protected void Awake() 
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Debug.LogError("There can't be two player camera controllers. Deleting this one.");
                Destroy(gameObject);
            }

            camera = GetComponent<Camera>();
            transform = gameObject.transform;
        }


        protected virtual void OnSetTarget(Transform transform) { }
        protected virtual void OnSetOffset(Vector2 offset) => this.offset = offset;
        protected virtual void OnSetExtraOffset(Vector2 offset) => this.extraOffset = offset;

        public static void SetTarget(Transform transform) => instance.OnSetTarget(transform);
        public static void SetOffset(Vector2 offset) => instance.OnSetOffset(offset);
        public static void SetExtraOffset(Vector2 offset) => instance.OnSetExtraOffset(offset);
    }
}