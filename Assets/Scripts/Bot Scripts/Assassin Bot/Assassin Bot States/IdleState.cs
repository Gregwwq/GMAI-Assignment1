using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class IdleState : FSMState<string>
    {
        const string Name = "Idle";

        public IdleState(FSM<string> _fsm) : base(_fsm, Name)
        {

        }

        public override void Enter()
        {
            Debug.Log("IDLE: waiting for targets...");
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