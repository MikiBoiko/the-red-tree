using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPLTV.Colosseum.Lobby
{
    [System.Serializable]
    public class ColosseumDataSelection<T> : ScriptableObject where T : ColosseumLobbyIcon
    {
        public T[] dataSheet;

        public bool ContainsAt(int index)
        {
            return index >= 0 && index < dataSheet.Length;
        }
    }
}