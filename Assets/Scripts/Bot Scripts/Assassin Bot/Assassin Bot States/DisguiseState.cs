using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class DisguiseState : FSMState<string>
    {
        const string Name = "Disguise";

        AssassinBotFSM main;

        GameObject original, disguise;

        float elap;

        public DisguiseState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;

            original = main.gameObject.transform.Find("Original").gameObject;
            disguise = main.gameObject.transform.Find("Disguise").gameObject;
        }

        public override void Enter()
        {
            elap = 0f;

            Debug.Log("DISGUISE: putting on a disguise...");
        }

        public override void Execute()
        {
            // adding a slight delay to signify putting on of the disguise
            if(elap >= 1f)
            {
                main.ChangeToDisguise();
                fsm.SetState("Select Weapon");
            }
            else elap += Time.deltaTime;
        }

        public override void Exit()
        {
            Debug.Log("DISGUISE: disguise done");
            Debug.Log("DISGUISE: " + main.DisguiseCount + " disguise left");
        }
    }
}