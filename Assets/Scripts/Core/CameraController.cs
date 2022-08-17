using UnityEditor;
using UnityEngine;

namespace NPLTV 
{
    public abstract class CameraController : MonoBehaviour
    {
        protected static CameraController _instance;

        [SerializeField] protected new Camera camera;
        [SerializeField] protected new Transform transform;

        [Range(0f, 1f)]
        public float lerpTime;

        protected void Awake() 
        {
            if(_instance == null)
            {
                _instance = this;
            }
            else if(_instance != this)
            {
                Debug.LogError("There can't be two player camera controllers. Deleting this one.");
                Destroy(gameObject);
            }

            camera = GetComponent<Camera>();
            transform = gameObject.transform;
        }


        protected virtual void OnSetTarget(Transform transform) { }
        protected virtual void OnSetOffset(Vector2 offset) { }
        protected virtual void OnSetExtraOffset(Vector2 offset) { }

        public static void SetTarget(Transform transform) => _instance.OnSetTarget(transform);
        public static void SetOffset(Vector2 offset) => _instance.OnSetOffset(offset);
        public static void SetExtraOffset(Vector2 offset) => _instance.OnSetExtraOffset(offset);
    }
}