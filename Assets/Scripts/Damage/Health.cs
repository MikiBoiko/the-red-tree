using UnityEngine;
using NPLTV.Movement;

namespace NPLTV.Damage
{
    [RequireComponent(typeof(Motor))]
    public class Health : MonoBehaviour, IDamagable, IKnockable
    {
        protected float _currentHealth, _maxHealth;
        [property:SerializeField] public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;

        // Posible futute division by zero
        public float HealthRatio => _currentHealth / MaxHealth;

        protected bool IsDead { private set; get; }

        public delegate void OnHealthChange();
        public event OnHealthChange OnDeath, OnDamage, OnHeal, OnRevive;

        [SerializeField] protected Motor _motor;

        #region Initialization
        protected void Awake() 
        {
            _motor = GetComponent<Motor>();    
        }

        public void SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            Revive();
            
        }
        #endregion

        #region Damagable
        public void Damage(float damageAmount) 
        {
            if(!IsDead && _maxHealth > 0) {
                //Debug.Log("Damaging.....");
                _currentHealth = Mathf.Clamp(_currentHealth - damageAmount, 0, _maxHealth);
                OnDamage();
                if(_currentHealth <= 0) 
                {
                    Die();
                }
            }    
        }

        public void FullHeal()
        {
            if(IsDead)
            {
                Revive();
            }

            _currentHealth = _maxHealth;
        }

        public void Heal(float healAmount) 
        {
            if(healAmount <= 0 && !IsDead) {
                _currentHealth = Mathf.Clamp(_currentHealth + healAmount, 0, _maxHealth);
                OnHeal();
            }
        }

        #endregion

        #region Knockeable
        public virtual void Knock(Vector2 direction, float amount, float time)
        {
            _motor.AddInstantForce(direction * amount);
            //Debug.Log("Knocking.....");
        }
        #endregion

        public virtual void Die() 
        {
            IsDead = true;
            OnDeath();
        }

        public virtual void Revive()
        {
            IsDead = false;
            OnRevive();
        }
    }
}

