using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class HighGroundState : FSMState<string>
    {
        const string Name = "High Ground";

        AssassinBotFSM main;

        Transform bot, highGround;
        Quaternion lookRotation;

        public HighGroundState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            Debug.Log("HIGH_GROUND: heading towards high ground");

            highGround = GameObject.Find("High Ground").transform;
        }

        public override void Execute()
        {
            // if the assasin bot has reached the high ground, change state
            if (Vector3.Distance(bot.position, highGround.position) <= 2f)
            {
                bot.position = highGround.Find("Standing Spot").position;
                fsm.SetState("Aim Sniper");
            }
            else
            {
                // setting the target rotation to face the high ground locations
                lookRotation = Quaternion.LookRotation((highGround.position - bot.position), Vector3.up);

                // moving towards high ground location
                bot.position =
                    Vector3.MoveTowards(bot.position, highGround.position, main.Speed * Time.deltaTime);
                
                // smoothly rotating towards high ground location
                bot.rotation =
                    Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
            }
        }

        public override void Exit()
        {
            Debug.Log("HIGH_GROUND: high ground has been taken");
        }
    }
}