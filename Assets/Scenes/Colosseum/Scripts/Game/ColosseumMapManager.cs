using System.Collections.Generic;
using UnityEngine;

namespace NPLTV.Colosseum.Game
{
    public class ColosseumMapManager : MonoBehaviour
    {
        [field: SerializeField] public int SizeX { private set; get; }
        [field: SerializeField] public int SizeY { private set; get; }

        [SerializeField]
        private List<Transform> _spawnPoints = new List<Transform>();
        private int _spawnIndex;

        private void Awake() {
            // Get spawn points
            Transform spawnPoints = transform.Find("Spawn Points");
            for (int i = 0; i < spawnPoints.childCount; i++)
            {
                _spawnPoints.Add(spawnPoints.GetChild(i));
            }
            Reset();
        }

        private void Reset() {
            _spawnIndex = 0;
        }

        private void Start()
        {
            ColosseumCameraController.SetMaxWidth(SizeX);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, new Vector2(SizeX, SizeY));
        }

        public Transform GetSpawnPoint() {
            Transform spawnPoint = _spawnPoints[_spawnIndex];
            _spawnIndex++;
            _spawnIndex %= _spawnPoints.Count;
            return spawnPoint;
        }
    }
}