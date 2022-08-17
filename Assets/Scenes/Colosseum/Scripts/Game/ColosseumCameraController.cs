using System.Collections;
using UnityEngine;

namespace NPLTV.Colosseum.Game.Cameras
{
    public class ColosseumCameraController : CameraController
    {
        
        [SerializeField]
        private Vector2 _center, _size;
        [SerializeField] private float _updateTick = 1f;

        private void Start()
        {
            StartCoroutine(UpdateCamera());
        }

        private IEnumerator UpdateCamera()
        {
            // For each player...
            float totalX = 0, totalY = 0, maxX = 0, maxY = 0;
            foreach (PlayerManager player in GameManager.Players)
            {
                // Add position to a total
                Vector2 playerPosition = player.Motor.GetPosition();
                totalX += playerPosition.x;
                totalY += playerPosition.y;

                // For each other player...
                foreach (PlayerManager other in GameManager.Players)
                {
                    if (player == other) continue;

                    // Get distance
                    Vector2 otherPosition = other.Motor.GetPosition();
                    Vector2 distance = new Vector2(
                        Mathf.Abs(playerPosition.x - otherPosition.x),
                        Mathf.Abs(playerPosition.y - otherPosition.y)
                    );

                    // Save it if its the max in
                    maxX = Mathf.Max(maxX, distance.x);
                    maxY = Mathf.Max(maxY, distance.y);
                }
            }

            _center = new Vector2(totalX, totalY) / GameManager.Players.Count;
            _size = new Vector2(maxX, maxY);

            yield return new WaitForSeconds(_updateTick);

            StartCoroutine(UpdateCamera());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_center, _size);
        }
    }
}