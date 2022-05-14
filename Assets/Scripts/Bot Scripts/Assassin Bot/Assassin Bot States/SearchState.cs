using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class SearchState : FSMState<string>
    {
        const string Name = "Search";

        AssassinBotFSM main;

        Transform bot;
        Quaternion lookRotation;

        GameObject[] targets;

        public SearchState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            Debug.Log("SEARCH: searching for a target bot to assassinate");
        }

        public override void Execute()
        {
            // find all targets in the area, calculate their average center location and head towards that
            targets = GameObject.FindGameObjectsWithTag("Target");

            float x = 0f;
            float z = 0f;

            foreach (GameObject target in targets)
            {
                if (Vector3.Distance(bot.position, target.transform.position) <= 15)
                {
                    main.EliminationTarget = target;
                    fsm.SetState("Disguise");
                    return;
                }

                x += target.transform.position.x;
                z += target.transform.position.z;
            }

            x /= targets.Length;
            z /= targets.Length;

            Vector3 targetLocation = new Vector3(x, bot.position.y, z);

            lookRotation = Quaternion.LookRotation((targetLocation - bot.position), Vector3.up);

            bot.position =
                Vector3.MoveTowards(bot.position, targetLocation, main.Speed * Time.deltaTime);
            
            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }

        public override void Exit()
        {
            bot.LookAt(new Vector3(main.EliminationTarget.transform.position.x, bot.position.y, main.EliminationTarget.transform.position.z));

            Debug.Log("SEARCH: target found!");
        }
    }
}