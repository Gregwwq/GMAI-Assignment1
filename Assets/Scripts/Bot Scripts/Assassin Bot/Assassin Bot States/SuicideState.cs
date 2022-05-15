using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class SuicideState : FSMState<string>
    {
        const string Name = "Suicide";

        AssassinBotFSM main;

        public SuicideState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            Debug.Log("SUICIDE: assassin has been caught, committing suicide to protect its secrets...");

            main.ChangeToDead();
            ResetTargetBots();

            Debug.Log("SUICIDE: assassin bot is dead");
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {

        }

        void ResetTargetBots()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

            foreach (GameObject target in targets)
            {
                target.GetComponent<TargetBotFSM>().ReturnToWander();
            }
        }
    }
}