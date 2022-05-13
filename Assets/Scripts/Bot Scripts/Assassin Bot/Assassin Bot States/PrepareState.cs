using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class PrepareState : FSMState<string>
    {
        const string Name = "Prepare";

        AssassinBotFSM main;

        Transform currentLocation, resupplyStation;
        Quaternion lookRotation;

        public PrepareState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;

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

                if (Vector3.Distance(currentLocation, resupplyStation) < 0.001f)
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
            currentLocation = main.gameObject.transform.position;
            lookRotation = Quaternion.LookRotation((resupplyStation - currentLocation), Vector3.up);

            main.gameObject.transform.position =
                Vector3.MoveTowards(currentLocation, resupplyStation, main.Speed * Time.deltaTime);

            main.gameObject.transform.rotation =
                Quaternion.RotateTowards(main.gameObject.transform.rotation, lookRotation, 200 * Time.deltaTime);
        }
    }
}