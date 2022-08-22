using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPLTV.Cameras
{
    public class SingleCameraController : CameraController
    {
        [SerializeField] private Transform _lockedTarget;

        private Vector2 _dampVelocity;

        [SerializeField]
        [Range(0f, 1f)]
        private float _positionLerpTime;

        private void FixedUpdate()
        {
            if (_lockedTarget != null)
            {
                Vector2 _newPosition = Vector2.SmoothDamp(transform.position, (Vector2)_lockedTarget.position + offset + extraOffset, ref _dampVelocity, _positionLerpTime);
                transform.position = new Vector3(_newPosition.x, _newPosition.y, -10);
            }
        }

        protected override void OnSetTarget(Transform transform) => _lockedTarget = transform;
    }
}
