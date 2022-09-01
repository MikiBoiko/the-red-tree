using UnityEngine;

namespace NPLTV.Colosseum.Lobby
{
    [System.Serializable]
    public class ColosseumMapIcon : ColosseumLobbyIcon
    {
        public string location;
        [TextArea] public string description;
        public int maxPlayers;
        public GameObject prefab;
    }

    [CreateAssetMenu(fileName = "New Map Sheet", menuName = "Colosseum/Map Sheet", order = 1)]
    public class ColosseumMapSelection : ColosseumDataSelection<ColosseumMapIcon> { }
}