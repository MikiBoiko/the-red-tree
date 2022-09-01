using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NPLTV.Colosseum.Lobby.Selectors;

namespace NPLTV.Colosseum.Lobby
{
    public enum ColosseumLobbySelectorType { character, map, gamemode };

    public class ColosseumLobbyManager : PlayerInputManager
    {
        private static ColosseumLobbyManager _instance;

        [Header("Player settings")]
        [SerializeField]
        private Transform _lobbyPlayersListTransform;

        [SerializeField] private List<ColosseumLobbyPlayer> _players;

        [SerializeField] private Color[] _playerColors;


        [Header("Player selections")]
        [SerializeField] private GameObject _selectionPrefab;
        [SerializeField]
        private ColosseumCharacterSelector _characterSelection;

        [SerializeField]
        private ColosseumMapSelector _mapSelection;

        [SerializeField]
        private ColosseumGameModeSelector _gameModeSelection;


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

            // Initialize data selectors
            _characterSelection.InitializeData(_selectionPrefab);
            _mapSelection.InitializeData(_selectionPrefab);
            _gameModeSelection.InitializeData(_selectionPrefab);
        }

        public static void JoinPlayer(ColosseumLobbyPlayer lobbyPlayer)
        {
            // Set parent
            lobbyPlayer.transform.SetParent(_instance._lobbyPlayersListTransform);

            // Set up
            int playerIndex = _instance._players.Count;
            lobbyPlayer.SetUp(_instance._playerColors[playerIndex], playerIndex);

            // Add to list
            _instance._players.Add(lobbyPlayer);
        }
    }
}