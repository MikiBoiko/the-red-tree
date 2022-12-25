using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace NPLTV.Colosseum.Lobby
{
    public enum ColosseumLobbyDirection 
    {
        None = 0,
        Right = 1,
        Up = 2,
        Left = 3,
        Down = 4
    };

    public class ColosseumLobbyPlayer : PlayerInput
    {
        [SerializeField] private Image _backgroundImage, _selectionImage, _readyImage;
        [SerializeField] private int _index;
        [SerializeField] private Text _playerText;
        [SerializeField] private Transform _indicator;

        // Vote indexes
        private ColosseumLobbySelectorType _currentSelector = ColosseumLobbySelectorType.character;
        public int character, map, gameMode;
        private ColosseumLobbyDirection _lastDirection;

        public bool Ready { private set; get; }

        private void Start() {
            SetUpInput();
        }

        private void SetUpInput()
        {
            ColosseumLobbyManager.JoinPlayer(this);

            actions.FindActionMap("Menu").Enable();
            currentActionMap.RemoveAllBindingOverrides();
            SwitchCurrentActionMap("Menu");

            currentActionMap["Ready"].performed += ctx =>
            {
                Debug.Log("Getting ready");
            };

            currentActionMap["Back"].performed += ctx => Back();
            
            currentActionMap["Selection"].performed += ctx =>
            {
                Selection(GetSelectionDirection(ctx.ReadValue<Vector2>()));
            };

            currentActionMap["Selection"].canceled += ctx => _lastDirection = ColosseumLobbyDirection.None; 

            currentActionMap["Select"].performed += ctx =>
            {
                Select();
                Debug.Log("Select");
            };
        }

        private ColosseumLobbyDirection GetSelectionDirection(Vector2 selection) 
        {
            if(selection.x > 0.5f)
                return ColosseumLobbyDirection.Right;
            if(selection.y > 0.5f)
                return ColosseumLobbyDirection.Up;
            if(selection.x < -0.5f)
                return ColosseumLobbyDirection.Left;
            if(selection.y < -0.5f)
                return ColosseumLobbyDirection.Down;
            return ColosseumLobbyDirection.None;
        }

        public void Reset() {
            _currentSelector = ColosseumLobbySelectorType.character;
            character = 0;
            map       = 0;
            gameMode  = 0;
            ColosseumLobbyManager.Selection(_currentSelector, 0, ColosseumLobbyDirection.None, _indicator);
            SetReady(false);
        }

        public void SetUp(Color color, int playerIndex)
        {
            _playerText.text = string.Format("Player {0}", playerIndex);
            _index = playerIndex;
            _backgroundImage.color = color;
            _indicator.GetComponent<Image>().color = color;
            Reset();
        }

        private void SetReady(bool to) {
            if(to) {
                _readyImage.color = Color.green;
                Ready = true;
                ColosseumLobbyManager.CheckForStart();
            }
            else {
                _readyImage.color = Color.gray;
                Ready = false;
            }
        }

        private void Disconnect() {
            StartCoroutine(DisconnectAsync());
        }

        private IEnumerator DisconnectAsync() {
            _indicator.SetParent(transform);
            yield return new WaitForFixedUpdate();
            ColosseumLobbyManager.DisconnectPlayer(this, _index);
        }

        private void Selection(ColosseumLobbyDirection direction) {
            if(direction == _lastDirection) return;
            _lastDirection = direction;
            if(direction == ColosseumLobbyDirection.None) return;
            switch (_currentSelector)
            {
                case ColosseumLobbySelectorType.character:
                    character = ColosseumLobbyManager.Selection(_currentSelector, character, direction, _indicator);
                    break;
                case ColosseumLobbySelectorType.map:
                    map = ColosseumLobbyManager.Selection(_currentSelector, map, direction, _indicator);
                    break;
                case ColosseumLobbySelectorType.gamemode:
                    if(Ready) return;
                    gameMode = ColosseumLobbyManager.Selection(_currentSelector, gameMode, direction, _indicator);
                    break;
            }
        }

        private void Select() {
            switch (_currentSelector)
            {
                case ColosseumLobbySelectorType.character:
                    ColosseumLobbyManager.Selection(ColosseumLobbySelectorType.map, 0, ColosseumLobbyDirection.None, _indicator);
                    _currentSelector = ColosseumLobbySelectorType.map;
                    break;
                case ColosseumLobbySelectorType.map:
                    ColosseumLobbyManager.Selection(ColosseumLobbySelectorType.gamemode, 0, ColosseumLobbyDirection.None, _indicator);
                    _currentSelector = ColosseumLobbySelectorType.gamemode;
                    break;
                case ColosseumLobbySelectorType.gamemode:
                    if(!Ready) SetReady(true);
                    break;
            }
        }

        private void Back() {
            switch (_currentSelector)
            {
                case ColosseumLobbySelectorType.character:
                    Disconnect();
                    break;
                case ColosseumLobbySelectorType.map:
                    map = 0;
                    character = ColosseumLobbyManager.Selection(ColosseumLobbySelectorType.character, character, ColosseumLobbyDirection.None, _indicator);
                    _currentSelector = ColosseumLobbySelectorType.character;
                    break;
                case ColosseumLobbySelectorType.gamemode:
                    if(Ready) {
                        SetReady(false);
                        return;
                    }
                    character = 0;
                    map = ColosseumLobbyManager.Selection(ColosseumLobbySelectorType.map, map, ColosseumLobbyDirection.None, _indicator);
                    _currentSelector = ColosseumLobbySelectorType.map;
                    break;
            }
        }
    }
}