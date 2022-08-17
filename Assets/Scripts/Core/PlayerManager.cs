using UnityEngine;
using NPLTV.Player;
using NPLTV.Interactable;
using NPLTV.Items;

namespace NPLTV
{
    [System.Serializable]
    public struct PlayerStats 
    {
        public float weight;
        public float hp;
        public float strength;
        public float agility;
        public float endurance;
    }

    public class PlayerManager : MonoBehaviour
    {
        [field: SerializeField] public PlayerStats Stats { private set; get; }
        [field: SerializeField] public PlayerAbilitySystem AbilitySystem { private set; get; }
        [field: SerializeField] public PlayerHealth Health { private set; get; }
        [field: SerializeField] public PlayerMotor Motor { private set; get; }
        [field: SerializeField] public PlayerAnimator Animator { private set; get; }
        [field: SerializeField] public PlayerStateInput StateInput { private set; get; }

        public Interactables interactables;

        [Header("Player inventory")]
        [SerializeField] private WeaponManager _selectedWeapon, _defaultWeapon;

        #region Initialization
        private void GetPlayerComponents()
        {
            Health = GetComponent<PlayerHealth>();
            Motor = GetComponent<PlayerMotor>();
            Animator = GetComponent<PlayerAnimator>();
            StateInput = GetComponent<PlayerStateInput>();
        }

        private void Awake()
        {
            GetPlayerComponents();

            interactables = new Interactables();
        }

        private void SetUpComponents()
        {
            // Set up 
            StateInput.SetUp(this);

            // Set up stats
            ApplyStats();

            // Set up weapon an abilities
            GameManager.AddPlayer(this);
            AbilitySystem.SetUp(this);
            SelectWeapon(null);
        }

        private void Start() 
        {
            SetUpComponents();
        }


        private void ApplyStats()
        {
            // Set weight
            Motor.SetWeight(Stats.weight);

            // Set hp
            Health.SetMaxHealth(Stats.hp);

            // Strength will be used in abilities or other things

            // Set agility
            Motor.jumpForce *= Stats.agility / 100;
            Motor.speed *= Stats.agility / 100;
            Motor.acceleratingForce *= Stats.agility / 100;

            // Set up endurance
            // TODO : after endurance system is done
        }
        #endregion

        #region PlayerInventory
        private void SelectWeapon(WeaponManager manager)
        {
            if(manager == null)
            {
                if(_selectedWeapon != _defaultWeapon)
                {
                    _selectedWeapon = _defaultWeapon;
                    AbilitySystem.SetCurrentWeapon(_selectedWeapon);
                }
            }
            else
            {
                if(_selectedWeapon != manager)
                {
                    _selectedWeapon = manager;
                    AbilitySystem.SetCurrentWeapon(_selectedWeapon);
                }
            }
        }
        #endregion
    }
}