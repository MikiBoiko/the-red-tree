using System.Collections.Generic;
using UnityEngine;
using NPLTV.Player.Abilities;

namespace NPLTV.Items
{
    public class WeaponManager : MonoBehaviour
    {
        public Dictionary<string, WeaponAbility> Abilities { private set; get; }

        [SerializeField] protected Weapon weapon;

        private void Awake() 
        {
            Abilities = new Dictionary<string, WeaponAbility>()
            {
                {
                    "basic",
                    new BasicActionAbility(
                        this,
                        0.2f,
                        Player.AbilityType.basic,
                        "Jab",
                        "Does a fast punch."
                    )
                },
                {
                    "side",
                    new BasicDamageAreaAbility(
                        this,
                        transform.Find("Side").GetComponent<Damage.DamageArea>(),
                        0.5f,
                        0.2f,
                        Player.AbilityType.special,
                        "Side Attack",
                        "The weapon ability attacking foward."
                    )
                },
                {
                    "cross",
                    new BasicActionAbility(
                        this,
                        1.5f,
                        Player.AbilityType.special,
                        "Haymaker",
                        "Something."
                    )
                }
            }; 
        }
    }
}
