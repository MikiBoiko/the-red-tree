using UnityEngine;

namespace NPLTV.Player.States
{
    public class DialogueState : PlayerState
    {
        public DialogueState(PlayerManager owner) : base(owner) { }

        public override void SetUp()
        {
            base.SetUp();

            CameraController.SetOffset(Vector2.zero);
        }

        public override void Move(Vector2 direction)
        {
            if (direction.y > 0.5f)
            {
                DialogueController.Instance.SelectUp();
            }
            else if (direction.y < -0.5f)
            {
                DialogueController.Instance.SelectDown();
            }
        }

        public override void Select()
        {
            DialogueController.Instance.ChooseSelectedOption();
        }

        public override void Jump()
        {
            DialogueController.Instance.ChooseSelectedOption();
        }

        public override bool IsCancelable() => false;
    }
}