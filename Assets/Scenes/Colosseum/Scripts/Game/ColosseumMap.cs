using UnityEngine;

namespace NPLTV.Colosseum
{
    public class ColosseumMap : MonoBehaviour
    {
        [field: SerializeField] public int SizeX { private set; get; }
        [field: SerializeField] public int SizeY { private set; get; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, new Vector2(SizeX, SizeY));
        }
    }
}