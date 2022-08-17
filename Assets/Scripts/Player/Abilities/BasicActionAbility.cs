using UnityEngine;

namespace NPLTV.Player.Abilities
{
    [System.Serializable]
    public class BasicActionAbility : WeaponAbility
    {
        protected States.ActionState actionState;
        protected float _time;

        public BasicActionAbility(Items.WeaponManager manager, float time, AbilityType type, string name, string description) :
            base(manager, type, name, description)
        {
            _time = time;
        }

        public override void SetUp(PlayerManager owner)
        {
            base.SetUp(owner);
            actionState = new States.ActionState(owner, name, _time);
        }

        protected override void OnPress()
        {
            owner.StateInput.SetState(actionState);
            base.OnPress();
        }
    }
}