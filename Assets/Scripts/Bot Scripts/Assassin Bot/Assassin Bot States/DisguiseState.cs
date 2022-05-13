using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class DisguiseState : FSMState<string>
    {
        const string Name = "Disguise";

        AssassinBotFSM main;

        GameObject bot;

        float elap;

        public DisguiseState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject;
        }

        public override void Enter()
        {
            elap = 0f;

            Debug.Log("DISGUISE: putting on a disguise...");
        }

        public override void Execute()
        {
            if(elap >= 1f)
            {
                fsm.SetState("Select Weapon");
            }
            else elap += Time.deltaTime;
        }

        public override void Exit()
        {
            Debug.Log("DISGUISE: disguise done");
        }
    }
}