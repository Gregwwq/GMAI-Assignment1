using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class InvisibleState : FSMState<string>
    {
        const string Name = "Invisible";

        AssassinBotFSM main;

        float elap;

        public InvisibleState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            elap = 0f;

            Debug.Log("INVISIBLE: going invisible now");
        }

        public override void Execute()
        {
            // becoming invisible after a short delay
            if (elap >= 0.5f)
            {
                main.ChangeToInvisible();
                fsm.SetState("Escape");
            }
            else elap += Time.deltaTime;
        }

        public override void Exit()
        {
            Debug.Log("INVISIBLE: invisibility successfully activated");
        }
    }
}