using UnityEngine;

namespace NPLTV.Player.States
{
    // TODO : hanging state
    public class HangingState : PlayerState
    {
        public HangingState(PlayerManager owner) : base(owner) { }

        public override void SetUp()
        {
            CameraController.SetOffset(Vector2.zero);
        }

        public override void Move(Vector2 direction) => owner.Motor.SetMovingDirection(direction);

        //protected override void Basic() => owner.AbilitySystem.GetAbility(AbilityType.basic).Press();
        //protected override void BasicReleased() => owner.AbilitySystem.GetAbility(AbilityType.basic).Release();
        //protected override void Special() => owner.AbilitySystem.GetAbility(AbilityType.special).Press();
        //protected override void SpecialReleased() => owner.AbilitySystem.GetAbility(AbilityType.special).Release();
        //protected override void Defense() => owner.AbilitySystem.GetAbility(AbilityType.defense).Press();
        //protected override void DefenseReleased() => owner.AbilitySystem.GetAbility(AbilityType.defense).Release();
        public override void Jump() => owner.AbilitySystem.GetJump().Press(); //owner.AbilitySystem.GetAbility(AbilityType.jump).Press();
        public override void JumpReleased() => owner.AbilitySystem.GetJump().Release();
        public override void Select() => owner.interactables.InteractWithSelected();
    }
}