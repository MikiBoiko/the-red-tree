using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using NPLTV.Colosseum.Game;
using NPLTV.Colosseum.Lobby.Selectors;

namespace NPLTV.Colosseum.Lobby
{
    public enum ColosseumLobbySelectorType 
    { 
        character = 0, 
        map = 1, 
        gamemode = 2
    };

    public class ColosseumLobbyManager : PlayerInputManager
    {
        private static ColosseumLobbyManager _instance;

        [Header("Player settings")]
        [SerializeField]
        private Transform _lobbyPlayersListTransform;

        [SerializeField] private List<ColosseumLobbyPlayer> _players;
        [SerializeField] private List<int> _availablePlayerIndexes;

        [SerializeField] private Color[] _playerColors;


        [Header("Player selections")]
        [SerializeField] private GameObject _selectionPrefab;
        [SerializeField]
        private ColosseumCharacterSelector _characterSelection;

        [SerializeField]
        private ColosseumMapSelector _mapSelection;

        [SerializeField]
        private ColosseumGameModeSelector _gameModeSelection;

        [Header("Other")]
        [SerializeField] private Scene _ingameScene; 
        [SerializeField] private Camera _lobbyCamera;


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

            _availablePlayerIndexes = new List<int>() { 1 };

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
            int playerIndex = _instance._availablePlayerIndexes[0];
            _instance._availablePlayerIndexes.Remove(0);
            if(!_instance._availablePlayerIndexes.Contains(playerIndex + 1))
                _instance._availablePlayerIndexes.Add(playerIndex + 1);
            lobbyPlayer.SetUp(_instance._playerColors[playerIndex - 1], playerIndex);

           // Add to list
            _instance._players.Add(lobbyPlayer);
        }

        public static void DisconnectPlayer(ColosseumLobbyPlayer lobbyPlayer, int playerIndex) {
            _instance._players.Remove(lobbyPlayer);
            _instance._availablePlayerIndexes.Insert(0, playerIndex);
            lobbyPlayer.DeactivateInput();
            lobbyPlayer.gameObject.SetActive(false);
            Destroy(lobbyPlayer.gameObject);
        }

        public static int Selection(ColosseumLobbySelectorType type, int from, ColosseumLobbyDirection direction, Transform indicator) {
            switch (type)
            {
                case ColosseumLobbySelectorType.character:
                    return _instance._characterSelection.SelectFrom(from, direction, indicator);
                case ColosseumLobbySelectorType.map:
                    return _instance._mapSelection.SelectFrom(from, direction, indicator);
                case ColosseumLobbySelectorType.gamemode:
                    return _instance._gameModeSelection.SelectFrom(from, direction, indicator);
                default:
                    Debug.LogError("Error with selector type.");
                    return 0;
            }
        }

        public static void CheckForStart() {
            bool start = true;
            foreach (ColosseumLobbyPlayer lobbyPlayer in _instance._players)
            {
                if(lobbyPlayer.Ready)
                    continue;
                
                start = false;
                break;
            }

            if(start) _instance.StartGame();
        }

        private void StartGame() {
            // Load game scene
            SceneManager.LoadScene("Ingame", LoadSceneMode.Additive);
            StartCoroutine(StartGameAsync());
        }

        private IEnumerator StartGameAsync() {
            yield return new WaitForFixedUpdate();
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));

            // Spawn map
            int selectedMapIndex = _players[Random.Range(0, _players.Count)].map;

            ColosseumMapManager map = 
                Instantiate(
                    (
                        selectedMapIndex == 0 ?
                        _mapSelection.GetRandomValue()
                        :
                        _mapSelection.GetValue(selectedMapIndex - 1)
                    ).prefab,
                    Vector3.zero,
                    Quaternion.identity
                ).GetComponent<ColosseumMapManager>();

            // Set gamemode
            // TODO

            // Spawn players
            foreach (ColosseumLobbyPlayer lobbyPlayer in _players)
            {
                // Instantiate player
                NPLTV.Player.PlayerStateInput playerStateInput = Instantiate(
                    (
                        lobbyPlayer.character == 0 ?
                        _characterSelection.GetRandomValue()
                        :
                        _characterSelection.GetValue(lobbyPlayer.character - 1)
                    ).prefab,
                    map.GetSpawnPoint().position,
                    Quaternion.identity
                ).GetComponent<NPLTV.Player.PlayerStateInput>();
            }

            // Disable lobby
            DisableJoining();
            _lobbyCamera.gameObject.SetActive(false);
        }
    }
}