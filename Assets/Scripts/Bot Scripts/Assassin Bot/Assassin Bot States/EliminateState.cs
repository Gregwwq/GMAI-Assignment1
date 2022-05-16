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
                // assassinating the elimination target
                targetScript.TriggerDie();

                // if the weapon used was sniper, moving the assassin bot back to ground level and using up 1 sniper bullet
                if (main.Weapon == AssassinBotFSM.Arsenal.Sniper)
                {
                    Transform highGround = GameObject.Find("High Ground").transform;
                    main.gameObject.transform.position = (highGround.position + (highGround.forward * 3));

                    main.SniperBullets--;
                    Debug.Log("ELIMINATE: " + main.SniperBullets + " sniper bullets left");
                }

                // if the weapon used was throwing knife, use up 1 throwing knife
                else if (main.Weapon == AssassinBotFSM.Arsenal.ThrowingKnife)
                {
                    main.ThrowingKnives--;
                    Debug.Log("ELIMINATE: " + main.SniperBullets + " throwing knives left");
                }

                // checking if there are any target bots in the scene
                GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
                if (targets.Length < 1)
                {
                    fsm.SetState("Idle");
                }
                // to test the SUICIDE state, comment out the line below and uncomment the line below that
                // by doing so, the assassin bot will fight back until it gets caught instead of running away
                //else fsm.SetState("Escape");
                else fsm.SetState("Idle");
            }
            else elap += Time.deltaTime;
        }

        public override void Exit()
        {
            Debug.Log("ELIMINATE: target has been eliminated");

            // clearing the elimination target and resetting everything back to default
            main.EliminationTarget = null;
            main.UnequipWeapon();
            main.ChangeToOriginal();

            // trigger the target bots to start chasing the assassin
            TriggerTargetBotsChase();
        }

        // function to make all alive target bots start chasing the assassin bot
        void TriggerTargetBotsChase()
        {
            // finding all target bots
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

            foreach (GameObject target in targets)
            {
                // triggering their chase state
                target.GetComponent<TargetBotFSM>().TriggerChase();
            }
        }
    }
}