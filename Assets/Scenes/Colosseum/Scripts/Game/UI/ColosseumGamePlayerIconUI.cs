using UnityEngine;
using UnityEngine.UI;

namespace NPLTV.Colosseum.Game.UI
{
    public class ColosseumGamePlayerIconUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        [SerializeField]
        private ColosseumBarIndicatorUI _healthIndicator, _enduranceIndicator;

        public void SetUp(PlayerManager player, Color color)
        {
            // Set up health indicator
            Player.PlayerHealth health = player.Health;

            // TODO : Future cool ass animations
            health.OnDamage += () =>
            {
                _healthIndicator.SetPercentage(health.HealthRatio);
            };

            health.OnDeath += () =>
            {
                _healthIndicator.SetPercentage(0);
            };

            health.OnHeal += () =>
            {
                _healthIndicator.SetPercentage(1);
            };

            health.OnRevive += () =>
            {
                _healthIndicator.SetPercentage(health.HealthRatio);
            };

            // Set up icon
            _icon.color = color;
        }
    }
}
