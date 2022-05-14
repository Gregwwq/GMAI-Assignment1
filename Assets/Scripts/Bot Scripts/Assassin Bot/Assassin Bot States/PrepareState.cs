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
            if (main.DisguiseCount == 0 || main.InvisCount == 0 || main.DecoyCount == 0)
            {
                MoveTowardsResupplyStation();

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

        void MoveTowardsResupplyStation()
        {
            Debug.Log("PREPARE: heading to the resupply station");
            
            lookRotation = Quaternion.LookRotation((resupplyStation.position - bot.position), Vector3.up);

            bot.position =
                Vector3.MoveTowards(bot.position, resupplyStation.position, main.Speed * Time.deltaTime);

            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }
    }
}