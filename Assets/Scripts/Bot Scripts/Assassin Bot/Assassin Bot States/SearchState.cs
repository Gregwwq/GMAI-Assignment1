using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class SearchState : FSMState<string>
    {
        const string Name = "Search";

        AssassinBotFSM main;

        Transform bot;

        public SearchState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            Debug.Log("SEARCH: searching for a target bot to assassinate");
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {

        }
    }
}