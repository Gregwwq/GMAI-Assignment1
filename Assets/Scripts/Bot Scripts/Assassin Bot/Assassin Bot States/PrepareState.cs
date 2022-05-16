using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class PrepareState : FSMState<string>
    {
        const string Name = "Prepare";

        AssassinBotFSM main;

        Transform bot, resupplyStation;
        Quaternion lookRotation;

        public PrepareState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;

            resupplyStation = GameObject.Find("Resupply Station").transform;
        }

        public override void Enter()
        {
            Debug.Log("PREPARE: checking if assassin has enough supplies");
        }

        public override void Execute()
        {
            // checking if assassin bot still has disguises, invisibility and decoys
            if (main.DisguiseCount == 0 || main.InvisCount == 0 || main.DecoyCount == 0)
            {
                MoveTowardsResupplyStation();

                // checking if the bot has arrived at the resupply station location
                if (Vector3.Distance(bot.position, resupplyStation.position) < 0.001f)
                {
                    fsm.SetState("Resupply");
                }
            }
            else
            {
                fsm.SetState("Search");
            }
        }

        public override void Exit()
        {
            Debug.Log("PREPARE: preparations complete");
        }

        // function for moving the bot towards the resupply station location
        void MoveTowardsResupplyStation()
        {
            Debug.Log("PREPARE: heading to the resupply station");
            
            // setting the target rotation of the bot
            lookRotation = Quaternion.LookRotation((resupplyStation.position - bot.position), Vector3.up);

            // using Vector3.MoveTowards() to move the bot from its current location to its destination
            bot.position =
                Vector3.MoveTowards(bot.position, resupplyStation.position, main.Speed * Time.deltaTime);

            // using Quaternion.RotateTowards() to smoothly rotate the bot to its target rotation
            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }
    }
}