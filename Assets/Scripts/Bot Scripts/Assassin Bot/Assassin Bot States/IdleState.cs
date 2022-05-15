using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class IdleState : FSMState<string>
    {
        const string Name = "Idle";

        AssassinBotFSM main;

        public IdleState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            Debug.Log("IDLE: waiting for targets...");

            main.InvisActive = false;
            main.DecoyActive = false;
        }

        public override void Execute()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
            if (targets.Length != 0) fsm.SetState("Prepare");
        }

        public override void Exit()
        {
            Debug.Log("IDLE: targets found!");
        }
    }
}