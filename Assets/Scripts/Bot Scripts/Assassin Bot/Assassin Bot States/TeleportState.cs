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
            target = main.EliminationTarget.transform;
            flippedRotation = target.eulerAngles + 180f * Vector3.up;
            elap = 0f;
            Debug.Log("TELEPORT: getting in range to teleport");
        }

        public override void Execute()
        {
            if (Vector3.Distance(bot.position, target.position) <= 3f)
            {
                Debug.Log("TELEPORT: teleporting...");

                if (elap >= 0.3f)
                {
                    bot.position = (target.position + target.forward);
                    
                    bot.eulerAngles = flippedRotation;

                    Debug.Log("TELEPORT: brandishing the sword...");
                    fsm.SetState("Eliminate");
                }
                else elap += Time.deltaTime;
            }
            else
            {
                lookRotation = Quaternion.LookRotation((target.position - bot.position), Vector3.up);

                bot.position =
                    Vector3.MoveTowards(bot.position, target.position, main.Speed * Time.deltaTime);

                bot.rotation =
                    Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
            }
        }

        public override void Exit()
        {

        }
    }
}