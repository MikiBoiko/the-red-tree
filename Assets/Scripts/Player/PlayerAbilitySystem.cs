using UnityEngine;
using NPLTV.Player.Abilities;
using NPLTV.Items;

namespace NPLTV.Player
{
    [System.Serializable]
    public class PlayerAbilitySystem
    {
        [SerializeField] protected PlayerManager owner;
        /// this will be updated to the THINK mechanic
        /// where the player will decide the controls tree
        [SerializeField] private JumpAbility _jumpAbility = new RegularJumpAbility("jump", "Regular jump with extra jumps in air.", 1);
        [SerializeField] private PlayerAbility __basicAbility, __specialAbility;

        public void SetUp(PlayerManager owner)
        {
            this.owner = owner;

            _jumpAbility.SetUp(owner);
        }

        public JumpAbility GetJump() => _jumpAbility;
        public PlayerAbility GetBasic() => __basicAbility;
        public PlayerAbility GetSpecial() => __specialAbility;
    
        public void SetCurrentWeapon(WeaponManager manager)
        {
            __basicAbility = manager.Abilities["basic"];
            __basicAbility.SetUp(owner);

            __specialAbility = manager.Abilities["cross"];
            __specialAbility.SetUp(owner);
        }

        public void CancelAbility()
        {

        }
    }
}