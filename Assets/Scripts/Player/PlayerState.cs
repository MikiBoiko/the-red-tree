using System;
using UnityEngine;
using UnityEngine.InputSystem;
using NPLTV.Console;

namespace NPLTV.Player
{
    [System.Serializable]
    public abstract class PlayerState
    {
        protected PlayerManager owner;

        public PlayerState(PlayerManager owner)
        {
            this.owner = owner;
        }

        public virtual void SetUp() { }
        public virtual void FixedUpdate() { }
        public virtual void ExitState() { }
        public virtual bool IsCancelable() => true;
        public virtual void Cancel() { }

        #region Direction (moving and aiming)
        public virtual void Move(Vector2 direction) { }
        public virtual void Aim(Vector2 direction)
        {
            CameraController.SetExtraOffset(direction * 3);
        }
        #endregion

        #region Abilities (basic. special, defense and jump)
        public virtual void Basic() { }
        public virtual void BasicReleased() { }
        public virtual void Special() { }
        public virtual void SpecialReleased() { }
        public virtual void Defense() { }
        public virtual void DefenseReleased() { }
        public virtual void Jump() { }
        public virtual void JumpReleased() { }
        #endregion

        #region Interact
        public virtual void Select() { }
        public virtual void Selection(Vector2 direction) 
        {
            if(direction != Vector2.zero)
            {
                if (direction.x > 0) owner.interactables.SelectNext();
                else if (direction.x < 0) owner.interactables.SelectBefore();
            }
        }
        #endregion
        public virtual void Enter() { }
        public virtual void Testing()
        {
            if (ConsoleController.IsOpen)
                ConsoleController.CloseConsole();
            else ConsoleController.OpenConsole();
        }
    }
}