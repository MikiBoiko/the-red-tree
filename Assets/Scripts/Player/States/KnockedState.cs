using UnityEngine;

namespace NPLTV.Player.States
{
    [System.Serializable]
    public class KnockedState : PlayerState
    {
        [SerializeField]
        private float _currentTime = 0;

        public KnockedState(PlayerManager owner, float knockedTime) : base(owner)
        {
            _currentTime = knockedTime;
        }
        public override void FixedUpdate()
        {
            if(_currentTime <= 0)
            {
                owner.StateInput.SetState("regular");
            }
            else
            {
                _currentTime -= Time.fixedDeltaTime;
            }
        }

        public override void SetUp()
        {
            base.SetUp();
            owner.Motor.enabled = false;
            Debug.Log("Knocked!");
        }

        public override void ExitState()
        {
            base.ExitState();
            owner.Motor.enabled = true;
            Debug.Log("Exit!");

        }
    }
}