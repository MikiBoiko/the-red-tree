using UnityEngine;

namespace NPLTV.Colosseum.Lobby
{
    [System.Serializable]
    public class ColosseumGameModeIcon : ColosseumLobbyIcon
    {
        public ColosseumGameMode gameMode;
    }

    [CreateAssetMenu(fileName = "New GameMode Sheet", menuName = "Colosseum/GameMode Sheet", order = 2)]
    public class ColosseumGameModeSelection : ColosseumDataSelection<ColosseumGameModeIcon> { }
}
