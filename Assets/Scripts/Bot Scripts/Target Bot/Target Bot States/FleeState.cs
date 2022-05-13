using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TargetBot
{
    public class FleeState : FSMState<string>
    {
        const string Name = "Flee";

        TargetBotFSM main;

        public FleeState(FSM<string> _fsm, TargetBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            main.ChangeColor(Color.red);
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {

        }
    }
}