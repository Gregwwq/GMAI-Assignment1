using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TargetBot
{
    public class FleeState : FSMState<string>
    {
        const string Name = "Flee";

        TargetBotFSM main;

        Transform bot, assassin;
        Quaternion lookRotation;

        public FleeState(FSM<string> _fsm, TargetBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            assassin = GameObject.Find("AssassinBot").transform;
            main.ChangeColor(Color.red);
        }

        public override void Execute()
        {
            Vector3 targetLocation = bot.position + (bot.position - assassin.position);

            lookRotation = Quaternion.LookRotation((targetLocation - bot.position), Vector3.up);

            bot.position =
                Vector3.MoveTowards(bot.position, targetLocation, main.Speed * Time.deltaTime);

            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }

        public override void Exit()
        {

        }
    }
}