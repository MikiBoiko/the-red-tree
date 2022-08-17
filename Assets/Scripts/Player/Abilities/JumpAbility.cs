using UnityEngine;

namespace NPLTV.Player.Abilities
{
    [System.Serializable]
    public abstract class JumpAbility : PlayerAbility
    {
        protected JumpAbility(string name, string description) : base(AbilityType.jump, name, description) { }
    }

    [System.Serializable]
    public class RegularJumpAbility : JumpAbility
    {
        [SerializeField] private int _currentExtraJumps, _maxExtraJumps;

        public RegularJumpAbility(string name, string description, int maxExtraJumps) : base(name, description) 
        {
            _maxExtraJumps = maxExtraJumps;
        }

        public override void SetUp(PlayerManager owner)
        {
            base.SetUp(owner);
            owner.Motor.OnGrounded += () => {
                _currentExtraJumps = _maxExtraJumps;
            };
        }

        protected override void OnPress()
        {
            if(owner.Motor.Grounded)
            {
                owner.Motor.Jump();
            }
            else if(_currentExtraJumps > 0)
            {
                _currentExtraJumps--;
                owner.Motor.Jump();
            }
        }
    }
}
