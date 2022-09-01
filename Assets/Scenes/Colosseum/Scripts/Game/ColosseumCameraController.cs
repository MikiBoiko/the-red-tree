using System.Collections;
using UnityEngine;

namespace NPLTV.Colosseum.Game
{
    public class ColosseumCameraController : CameraController
    {
        [SerializeField] private float _updateTick = 1f;
        [SerializeField] [Range(0f, 1f)] private float _positionLerpTime, _sizeLerpTime;
        [SerializeField] private float _minSize, _maxSize;
        [SerializeField] [Range(1f, 10f)] private float _dSize = 2.5f;

        private void Start()
        {
            StartCoroutine(UpdateCamera());
        }

        private void FixedUpdate()
        {
            if (GameManager.Players.Count == 0 || GameManager.Players == null) return;

            // Update camera size
            float clampledSize = Mathf.Clamp(size.x / _dSize, _minSize, _maxSize);

            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, clampledSize, _sizeLerpTime);
            
            // Update camera position
            Vector3 cameraPosition = center + offset + extraOffset;
            float range = (_maxSize - clampledSize) * camera.aspect;
            cameraPosition = new Vector3(
                Mathf.Clamp(cameraPosition.x, -range , range),
                cameraPosition.y,
                -10
            );

            transform.position = Vector3.Lerp(
                transform.position, 
                cameraPosition, 
                _positionLerpTime
            );

            // Set offset
            SetOffset(Vector2.Max(Vector2.up * clampledSize / 2, Vector2.up * 2));
        }

        public static void SetMaxWidth(float maxWidth)
        {
            ColosseumCameraController cameraController = instance as ColosseumCameraController;
            cameraController._maxSize = maxWidth / cameraController.camera.aspect / 2;
        }

        private IEnumerator UpdateCamera()
        {
            if (GameManager.Players.Count >= 0)
            {
                // For each player...
                float totalX = 0, totalY = 0, maxX = int.MinValue, maxY = int.MinValue, minX = int.MaxValue, minY = int.MaxValue;
                foreach (PlayerManager player in GameManager.Players)
                {
                    // Add position to a total
                    Vector2 playerPosition = player.Motor.GetPosition();
                    totalX += playerPosition.x;
                    totalY += playerPosition.y;

                    // Save it if its the max in
                    maxX = Mathf.Max(maxX, playerPosition.x);
                    maxY = Mathf.Max(maxY, playerPosition.y);
                    minX = Mathf.Min(minX, playerPosition.x);
                    minY = Mathf.Min(minY, playerPosition.y);
                }

                center = new Vector2(totalX, totalY) / GameManager.Players.Count;
                size = new Vector2(maxX - minX, maxY - minY);

                yield return new WaitForSeconds(_updateTick);
            
            }

            StartCoroutine(UpdateCamera());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, size);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(center, 0.5f);
        }
    }
}