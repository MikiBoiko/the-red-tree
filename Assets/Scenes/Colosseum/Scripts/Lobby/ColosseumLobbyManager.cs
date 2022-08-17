using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NPLTV.Colosseum
{
    public class ColosseumLobbyManager : PlayerInputManager
    {
        private static ColosseumLobbyManager _instance;

        [SerializeField]
        private List<ColosseumLobbyPlayer> _players;

        private void Awake() 
        {
            if (_instance == null && _instance != this)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError("Can't instantiate two ColosseumLobbyUIManagers at the same time.");
                Destroy(gameObject);
            }
        }

        public static void JoinPlayer(ColosseumLobbyPlayer lobbyPlayer)
        {
            ColosseumLobbyUIManager.JoinPlayer(lobbyPlayer);
            _instance._players.Add(lobbyPlayer);
        }
    }
}