using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class ResupplyState : FSMState<string>
    {
        const string Name = "Resupply";

        AssassinBotFSM main;

        Transform interactSpot;

        float elap;

        public ResupplyState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            Debug.Log("RESUPPLY: gathering supplies");

            interactSpot = GameObject.Find("Resupply Station").transform.Find("Interact Spot");

            main.gameObject.transform.position = interactSpot;

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
                fsm.SetState("Search");
            }
        }

        public override void Exit()
        {

        }
    }
}