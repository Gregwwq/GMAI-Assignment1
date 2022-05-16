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
                // if any one of the target bots are within 15m, set it as the elimination target and change state
                if (Vector3.Distance(bot.position, target.transform.position) <= 15)
                {
                    main.EliminationTarget = target;
                    fsm.SetState("Disguise");
                    return;
                }

                // calculating the total x and z positions of all the target bots
                x += target.transform.position.x;
                z += target.transform.position.z;
            }

            // gettting the average x and z positions of all the target bots
            x /= targets.Length;
            z /= targets.Length;

            // setting the average position as the target location
            Vector3 targetLocation = new Vector3(x, bot.position.y, z);

            // setting the target rotation
            lookRotation = Quaternion.LookRotation((targetLocation - bot.position), Vector3.up);

            // moving towards the target location
            bot.position =
                Vector3.MoveTowards(bot.position, targetLocation, main.Speed * Time.deltaTime);
            
            // smoothly rotating to the target rotation
            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }

        public override void Exit()
        {
            // facing the elimination target
            bot.LookAt(new Vector3(main.EliminationTarget.transform.position.x, bot.position.y, main.EliminationTarget.transform.position.z));

            Debug.Log("SEARCH: target found!");
        }
    }
}