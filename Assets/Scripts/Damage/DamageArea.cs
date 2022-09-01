using System.Collections;
using UnityEngine;

namespace NPLTV.Damage
{   
    [RequireComponent(typeof(Collider2D))]
    public class DamageArea : MonoBehaviour 
    {
        [SerializeField] private Collider2D _trigger;
        [SerializeField] private GameObject _spriteGameObject = null;
        [SerializeField] private float _knockForce, _knockTime, _damageAmount;

        private void Awake() 
        {
            _trigger = GetComponent<Collider2D>();
            _spriteGameObject = transform.GetChild(0).gameObject;
        }

        public virtual void Activate(float delay, float holdFor)
        {
            StartCoroutine(ActivateFor(delay, holdFor));
        }

        private void OnEnable()
        {
            _trigger.enabled = true;
            _spriteGameObject?.SetActive(true);
        }

        private void OnDisable()
        {
            _trigger.enabled = false;
            _spriteGameObject?.SetActive(false);
        }

        private IEnumerator ActivateFor(float delay, float holdFor)
        {
            yield return new WaitForSeconds(delay);
            this.enabled = true;
            yield return new WaitForSeconds(holdFor);
            this.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            IKnockable knockeable = other.GetComponent<IKnockable>();
            knockeable?.Knock(transform.up, _knockForce, _knockTime);

            IDamagable damagable = other.GetComponent<IDamagable>();
            damagable?.Damage(_damageAmount);
            //Debug.Log(knockeable);
            //Debug.Log(damagable);
            //Debug.Log(other);
        }
    }
}