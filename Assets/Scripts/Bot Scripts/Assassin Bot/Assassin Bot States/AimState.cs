using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class AimState : FSMState<string>
    {
        const string Name = "Aim Sniper";

        AssassinBotFSM main;

        public AimState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            Debug.Log("AIM: taking aim at the elimination target");
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {

        }
    }
}