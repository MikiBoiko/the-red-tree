using UnityEngine;

namespace NPLTV.Player
{
    public enum AbilityType 
    {
        basic = 0,
        special = 1,
        defense = 2,
        jump = 3
    }

    [System.Serializable]
    public abstract class PlayerAbility 
    {
        protected PlayerManager owner;

        [SerializeField] private AbilityType _type;
        [SerializeField] protected string name;
        [SerializeField] [TextArea] 
        private string _description;

        protected PlayerAbility(AbilityType type, string name, string description)
        {
            _type = type;
            this.name = name;
            _description = description;
        }

        public bool CompareAbilityType(AbilityType type) => _type == type;

        public virtual void SetUp(PlayerManager owner)
        {
            Debug.Log("Testing " + name + " " + _type);
            this.owner = owner;
        }

        public void Press()
        {
            OnPress();
        }

        public void Release()
        {
            OnRelease();
        }

        protected virtual void OnPress() { }
        protected virtual void OnRelease() { }
    }
}
