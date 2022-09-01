using UnityEngine;

namespace NPLTV.Colosseum.Game
{
    public class ColosseumMapManager : MonoBehaviour
    {
        [field: SerializeField] public int SizeX { private set; get; }
        [field: SerializeField] public int SizeY { private set; get; }

        private void Start()
        {
            ColosseumCameraController.SetMaxWidth(SizeX);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, new Vector2(SizeX, SizeY));
        }
    }
}