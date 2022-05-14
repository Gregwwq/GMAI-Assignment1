using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class ResupplyState : FSMState<string>
    {
        const string Name = "Resupply";

        AssassinBotFSM main;

        float elap;

        public ResupplyState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            Debug.Log("RESUPPLY: gathering supplies");

            elap = 0f;
        }

        public override void Execute()
        {
            if (elap <= 3f)
            {
                Debug.Log("RESUPPLY: resupplying... time left: " + Mathf.Round(3f - elap) + "s");
                
                elap += Time.deltaTime;
            }
            else
            {
                main.DisguiseCount = 5;
                main.InvisCount = 5;
                main.DecoyCount = 5;

                main.SniperBullets = 1;
                main.ThrowingKnives = 2;

                fsm.SetState("Search");
            }
        }

        public override void Exit()
        {
            Debug.Log("RESUPPLY: resupply completed");
        }
    }
}