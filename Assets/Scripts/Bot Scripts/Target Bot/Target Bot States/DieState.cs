using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TargetBot
{
    public class DieState : FSMState<string>
    {
        const string Name = "Die";

        TargetBotFSM main;

        public DieState(FSM<string> _fsm, TargetBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {

        }
    }
}