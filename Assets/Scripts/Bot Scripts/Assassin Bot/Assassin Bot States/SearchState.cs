using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class SearchState : FSMState<string>
    {
        const string Name = "Search";

        AssassinBotFSM main;

        public SearchState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
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