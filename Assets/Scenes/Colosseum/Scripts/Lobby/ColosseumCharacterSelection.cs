using UnityEngine;

namespace NPLTV.Colosseum.Lobby
{
    [System.Serializable]
    public class ColosseumCharacterIcon : ColosseumLobbyIcon
    {
        public string epithet;
        public GameObject prefab;
    }

    [CreateAssetMenu(fileName = "New Character Sheet", menuName = "Colosseum/Character Sheet", order = 0)]
    public class ColosseumCharacterSelection : ColosseumDataSelection<ColosseumCharacterIcon> { }
}
