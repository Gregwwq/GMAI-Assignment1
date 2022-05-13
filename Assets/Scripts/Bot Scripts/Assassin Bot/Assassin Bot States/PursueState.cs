using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class PursueState : FSMState<string>
    {
        const string Name = "Pursue";

        AssassinBotFSM main;

        Transform bot;

        public PursueState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
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