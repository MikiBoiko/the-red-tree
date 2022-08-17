using UnityEngine;

namespace NPLTV.Player.States
{
    public class RegularState : PlayerState
    {
        public RegularState(PlayerManager owner) : base(owner) { }

        public override void SetUp()
        {
            CameraController.SetOffset(Vector2.up * 3);

            Move(owner.StateInput.MoveValue);
            Aim(owner.StateInput.AimValue);
        }

        public override void ExitState()
        {
            Move(Vector2.zero);
            Aim(Vector2.zero);
        }
        public override void Move(Vector2 direction) => owner.Motor.SetMovingDirection(direction);

        public override void Basic() => owner.AbilitySystem.GetBasic().Press();
        public override void BasicReleased() => owner.AbilitySystem.GetBasic().Release();
        public override void Special() => owner.AbilitySystem.GetSpecial().Press();
        public override void SpecialReleased() => owner.AbilitySystem.GetSpecial().Release();
        //protected override void Defense() => owner.AbilitySystem.GetAbility(AbilityType.defense).Press();
        //protected override void DefenseReleased() => owner.AbilitySystem.GetAbility(AbilityType.defense).Release();
        public override void Jump() => owner.AbilitySystem.GetJump().Press(); //owner.AbilitySystem.GetAbility(AbilityType.jump).Press();
        public override void JumpReleased() => owner.AbilitySystem.GetJump().Release();

        public override void Select() => owner.interactables.InteractWithSelected();
    
        public override bool IsCancelable() => false;
    }
}