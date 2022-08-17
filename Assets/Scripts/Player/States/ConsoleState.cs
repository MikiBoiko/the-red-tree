using UnityEngine;
using NPLTV.Console;

namespace NPLTV.Player.States
{
    public class ConsoleState : PlayerState
    {
        public ConsoleState(PlayerManager owner) : base(owner) { }

        public override void Enter() => ConsoleController.Enter();
    }
}