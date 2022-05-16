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
            // setting the target location to be slightly infront of the target bot
            // when it is facing away from the assassin bot
            Vector3 targetLocation = bot.position + (bot.position - assassin.position);

            // setting the target rotation to face the target location
            lookRotation = Quaternion.LookRotation((targetLocation - bot.position), Vector3.up);

            // moving towards the target location
            bot.position =
                Vector3.MoveTowards(bot.position, targetLocation, main.Speed * Time.deltaTime);

            // smoothly rotating towards the target rotation
            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }

        public override void Exit()
        {

        }
    }
}