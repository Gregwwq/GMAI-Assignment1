using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class EliminateState : FSMState<string>
    {
        const string Name = "Eliminate";

        AssassinBotFSM main;
        TargetBotFSM targetScript;

        float elap;

        public EliminateState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            elap = 0f;
            targetScript = main.EliminationTarget.GetComponent<TargetBotFSM>();
            Debug.Log("ELIMINATE: eliminating the target...");
        }

        public override void Execute()
        {
            if (elap >= 0.5f)
            {
                targetScript.TriggerDie();

                if (main.Weapon == AssassinBotFSM.Arsenal.Sniper)
                {
                    Transform highGround = GameObject.Find("High Ground").transform;
                    main.gameObject.transform.position = (highGround.position + (highGround.forward * 3));

                    main.SniperBullets--;
                    Debug.Log("ELIMINATE: " + main.SniperBullets + " sniper bullets left");
                }
                else if (main.Weapon == AssassinBotFSM.Arsenal.ThrowingKnife)
                {
                    main.ThrowingKnives--;
                    Debug.Log("ELIMINATE: " + main.SniperBullets + " throwing knives left");
                }

                fsm.SetState("Escape");
            }
            else elap += Time.deltaTime;
        }

        public override void Exit()
        {
            Debug.Log("ELIMINATE: target has been eliminated");
            main.EliminationTarget = null;
            main.UnequipWeapon();
            main.ChangeToOriginal();

            TriggerTargetBotsChase();
        }

        void TriggerTargetBotsChase()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

            foreach (GameObject target in targets)
            {
                target.GetComponent<TargetBotFSM>().TriggerChase();
            }
        }
    }
}