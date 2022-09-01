using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace NPLTV.Colosseum.Lobby
{
    public class ColosseumLobbyPlayer : PlayerInput
    {
        [SerializeField] private Image _backgroundImage, _selectionImage, _readyImage;
        [SerializeField] private Text _playerText;

        // Vote indexes
        private ColosseumLobbySelectorType _currentSelector = ColosseumLobbySelectorType.character;
        private int _character = 0;
        private int _map       = 0;
        private int _gameMode  = 0;

        public bool Ready { private set; get; }

        private void Start()
        {
            ColosseumLobbyManager.JoinPlayer(this);

            actions.FindActionMap("Menu").Enable();
            SwitchCurrentActionMap("Menu");

            currentActionMap["Ready"].performed += ctx =>
            {
                Debug.Log("Getting ready");
            };

            currentActionMap["Back"].performed += ctx =>
            {
                Debug.Log("Back");
            };
            
            currentActionMap["Selection"].performed += ctx =>
            {
                Debug.Log("Selection " + ctx.ReadValue<Vector2>());
            };

            currentActionMap["Select"].performed += ctx =>
            {
                Debug.Log("Select");
            };
        }

        public void Reset() {
            _currentSelector = ColosseumLobbySelectorType.character;
            _character = 0;
            _map       = 0;
            _gameMode  = 0;
        }

        public void SetUp(Color color, int playerIndex)
        {
            _playerText.text = string.Format("Player {0}", playerIndex + 1);
            _backgroundImage.color = color;
        }
    }
}