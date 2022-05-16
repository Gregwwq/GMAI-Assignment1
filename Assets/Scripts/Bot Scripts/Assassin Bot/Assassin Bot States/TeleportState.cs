using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class TeleportState : FSMState<string>
    {
        const string Name = "Teleport";

        AssassinBotFSM main;

        Transform bot, target;
        Quaternion lookRotation;
        Vector3 flippedRotation;

        float elap;

        public TeleportState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            elap = 0f;
            target = main.EliminationTarget.transform;
            Debug.Log("TELEPORT: getting in range to teleport");

            // setting the flipped rotation along the y axis of the elimination target
            flippedRotation = target.eulerAngles + (180f * Vector3.up);
        }

        public override void Execute()
        {
            // checking if the elimination target is within 3m of the assassin bot
            if (Vector3.Distance(bot.position, target.position) <= 3f)
            {
                Debug.Log("TELEPORT: teleporting...");

                // short delay before teleportation and change state
                if (elap >= 0.3f)
                {
                    // setting the assassin bot's position to slightly infront of the elimination target
                    bot.position = (target.position + target.forward);
                    
                    // making the assassin bot face the elimination target
                    bot.eulerAngles = flippedRotation;

                    Debug.Log("TELEPORT: brandishing the sword...");
                    fsm.SetState("Eliminate");
                }
                else elap += Time.deltaTime;
            }
            else
            {
                // setting the target rotation to face the elimination target
                lookRotation = Quaternion.LookRotation((target.position - bot.position), Vector3.up);

                // moving towards the elimination target
                bot.position =
                    Vector3.MoveTowards(bot.position, target.position, main.Speed * Time.deltaTime);

                // smoothly rotating to face the elimination target
                bot.rotation =
                    Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
            }
        }

        public override void Exit()
        {

        }
    }
}