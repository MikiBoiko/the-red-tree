using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPLTV.Cameras
{
    public class SingleCameraController : CameraController
    {
        [SerializeField] private Transform _lockedTarget;

        [SerializeField] protected Vector2 _offset, _extraOffset;
        private Vector2 _dampVelocity;

        private void FixedUpdate()
        {
            if (_lockedTarget != null)
            {
                Vector2 _newPosition = Vector2.SmoothDamp(transform.position, (Vector2)_lockedTarget.position + _offset + _extraOffset, ref _dampVelocity, lerpTime);
                transform.position = new Vector3(_newPosition.x, _newPosition.y, -10);
            }
        }

        protected override void OnSetTarget(Transform transform) => _lockedTarget = transform;
        protected override void OnSetOffset(Vector2 offset) => _offset = offset;
        protected override void OnSetExtraOffset(Vector2 offset) => _extraOffset = offset;
    }
}
