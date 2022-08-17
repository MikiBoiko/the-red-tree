using NPLTV.Player.States;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NPLTV.Player
{
    [System.Serializable]
    public class PlayerStateInput : PlayerInput
    {
        [Header("Input")]
        [SerializeField] private InputActionMap map;
        [field: SerializeField] public Vector2 MoveValue { private set; get; }
        [field: SerializeField] public Vector2 AimValue { private set; get; }


        [Header("State")]
        [SerializeField] private PlayerState _state, _previousState;
        private PlayerState State => _state;
        [SerializeField] private Dictionary<string, PlayerState> _states;

        private void Start() 
        {
            map = actions.FindActionMap("Game");
            map.Enable();

            // Joysticks
            map["Move"].performed += ctx =>
            {
                Vector2 value = ctx.ReadValue<Vector2>();
                State?.Move(value);
                MoveValue = value;
            };
            map["Move"].canceled += ctx =>
            {
                State?.Move(Vector2.zero);
                MoveValue = Vector2.zero;
            };
            map["Aim"].performed += ctx =>
            {
                Vector2 value = ctx.ReadValue<Vector2>();
                State?.Aim(value);
                AimValue = value;
            };
            map["Aim"].canceled += ctx =>
            {
                State?.Aim(Vector2.zero);
                AimValue = Vector2.zero;
            };

            // Buttons
            map["Basic"].performed += ctx => _state?.Basic();
            map["Basic"].canceled += ctx => _state?.BasicReleased();
            map["Special"].performed += ctx => _state?.Special();
            map["Special"].canceled += ctx => _state?.SpecialReleased();
            map["Defense"].performed += ctx => _state?.Defense();
            map["Defense"].canceled += ctx => _state?.DefenseReleased();
            map["Jump"].performed += ctx => _state?.Jump();
            map["Jump"].canceled += ctx => _state?.JumpReleased();

            // Other
            map["Select"].performed += ctx => _state?.Select();
            map["Selection"].performed += ctx => _state?.Selection(ctx.ReadValue<Vector2>());
            map["Enter"].performed += ctx => _state?.Enter();
            map["Testing"].performed += ctx => _state?.Testing();
        }
        public void SetUp(PlayerManager owner)
        {
            _states = new Dictionary<string, PlayerState>()
            {
                { "regular",  new RegularState(owner)  },
                { "dialogue", new DialogueState(owner) },
                { "console",  new ConsoleState(owner)  }
            };

            // Set player to regular state
            SetState("regular");
        }

        private void FixedUpdate()
        {
            _state?.FixedUpdate();
        }

        #region PlayerState
        public void SetState(PlayerState state)
        {
            if (state != null && _state != state)
            {
                _state?.ExitState();
                state.SetUp();
                _previousState = _state;
                _state = state;
            }
        }

        public void SetState(string stateName)
        {
            if (_states.ContainsKey(stateName))
            {
                SetState(_states[stateName]);
            }
            else Debug.LogError($"Player state { stateName } not found");
        }

        public void SetPreviousState()
        {
            if (_previousState.IsCancelable()) return;

            if (_previousState != null) SetState(_previousState);
            else SetState("regular");
        }
        #endregion
    }
}