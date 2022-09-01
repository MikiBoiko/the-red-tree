using UnityEngine;
using NPLTV.Damage;
using NPLTV.Player.States;

namespace NPLTV.Player 
{
    [RequireComponent(typeof(PlayerManager))]
    [System.Serializable]
    public class PlayerHealth : Health
    {
        private PlayerManager _manager;

        private new void Awake() 
        {
            base.Awake();

            _manager = GetComponent<PlayerManager>();
            _motor = _manager.Motor;

            OnDeath += () => {
                Debug.Log("Player died!");
            };

            OnDamage += () => {
                Debug.Log("Player damaged!");
            };

            OnHeal += () => {
                Debug.Log("Player healed!");
            };

            OnRevive += () => {
                Debug.Log("Player revived!");
            };
        }

        public override void Knock(Vector2 force, float amount, float time)
        {
            base.Knock(force, amount, time);
            _manager.StateInput.SetState(new KnockedOutState(_manager, time));
        }
    }
}
