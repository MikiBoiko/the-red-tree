using UnityEngine;
using NPLTV.Damage;

namespace NPLTV.Player.Abilities
{
    [System.Serializable]
    public class BasicDamageAreaAbility : WeaponAbility
    {
        protected DamageArea damageArea;
        protected float delay, holdFor;

        public BasicDamageAreaAbility (Items.WeaponManager manager, DamageArea damageArea, float delay, float holdFor, AbilityType type, string name, string description) : 
            base(manager, type, name, description)
        {
            this.damageArea = damageArea;
            this.delay = delay;
            this.holdFor = holdFor;
        }

        protected override void OnPress()
        {
            damageArea.Activate(delay, holdFor);
            Debug.Log("Doing");
        }
    }
}