using UnityEngine;

namespace NPLTV.Colosseum.Lobby
{
    [System.Serializable]
    public struct Character
    {
        public string name;
        public Sprite icon;
        public GameObject prefab;
    }

    [CreateAssetMenu(fileName = "Character Sheet", menuName = "Colosseum/Character Sheet", order = 0)]
    public class ColosseumCharacterSelection : ScriptableObject
    {
        public Character[] characters;
    }
}
