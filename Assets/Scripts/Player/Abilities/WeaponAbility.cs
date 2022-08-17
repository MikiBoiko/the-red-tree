using NPLTV.Items;

namespace NPLTV.Player.Abilities
{
    [System.Serializable]
    public class WeaponAbility : PlayerAbility
    {
        protected WeaponManager manager;

        public WeaponAbility (WeaponManager manager, AbilityType type, string name, string description) : 
            base(type, name, description)
        {
            this.manager = manager;
        }

    }
}