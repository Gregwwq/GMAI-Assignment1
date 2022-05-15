using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class PursueState : FSMState<string>
    {
        const string Name = "Pursue";

        AssassinBotFSM main;

        Transform bot, target;
        Quaternion lookRotation;

        float elap;

        public PursueState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            target = main.EliminationTarget.transform;
            elap = 0f;
            Debug.Log("PURSUE: chasing down the target!");
        }

        public override void Execute()
        {
            if (Vector3.Distance(bot.position, target.position) <= 10f)
            {
                target.gameObject.GetComponent<TargetBotFSM>().TriggerTargeted();
            }
            
            if (Vector3.Distance(bot.position, target.position) <= 6f)
            {
                if (main.Weapon == AssassinBotFSM.Arsenal.ThrowingKnife)
                {
                    Debug.Log("PURSUE: getting ready to throw the knife");
                    
                    if (elap >= 0.5f)
                    {
                        fsm.SetState("Eliminate");
                    }
                    else elap += Time.deltaTime;
                }
                else if (main.Weapon == AssassinBotFSM.Arsenal.Sword)
                {
                    fsm.SetState("Teleport");
                }
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