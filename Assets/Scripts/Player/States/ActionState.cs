using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPLTV.Player.States
{
    [System.Serializable]
    public class ActionState : PlayerState
    {
        [SerializeField]
        private float _currentTime = 0, _maxTime;
        [SerializeField]
        private string _name;

        public ActionState(PlayerManager owner, string name, float time) : base(owner) 
        {
            _name = name;
            _maxTime = time;
        }

        public override void FixedUpdate()
        {
            if (_currentTime <= 0)
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
            _currentTime = _maxTime;
            Debug.Log(owner);
            owner.Animator.SetAction(_name, 1 / _maxTime);
            Debug.Log("Action!");
        }

        public override void ExitState()
        {
            base.ExitState();
            Debug.Log("Exit action!");
        }

        public override void Cancel()
        {
            base.Cancel();
        }
    }
}
