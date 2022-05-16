using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class AimState : FSMState<string>
    {
        const string Name = "Aim Sniper";

        AssassinBotFSM main;

        Transform bot;

        float elap;

        public AimState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            elap = 0f;

            Debug.Log("AIM: taking aim at the elimination target");
        }

        public override void Execute()
        {
            // aiming at the head of the elimination target for a moment before changing state
            if (elap >= 1.9f)
            {
                fsm.SetState("Eliminate");
            }
            else
            {
                Debug.Log("AIM: firing in " + Mathf.Round(3f - elap));

                bot.LookAt(new Vector3(main.EliminationTarget.transform.position.x, bot.position.y, main.EliminationTarget.transform.position.z));

                Debug.DrawRay(bot.Find("Disguise").Find("Head").position, (main.EliminationTarget.transform.Find("Head").position - bot.Find("Disguise").Find("Head").position), Color.red);

                elap += Time.deltaTime;
            }

        }

        public override void Exit()
        {

        }
    }
}