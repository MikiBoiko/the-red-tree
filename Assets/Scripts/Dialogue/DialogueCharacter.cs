using UnityEngine;

namespace NPLTV.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue Character", menuName = "NPLTV/Dialogue/New Character", order = 0)]
    public class DialogueCharacter : ScriptableObject
    {
        public string characterName;
        public Sprite icon;
    }
}
