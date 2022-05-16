using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class EscapeState : FSMState<string>
    {
        const string Name = "Escape";

        AssassinBotFSM main;

        GameObject[] targets;
        Transform bot;
        Vector3 centralLocation, nearestTarget, moveLocation;
        Quaternion lookRotation;

        float elap;

        public EscapeState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            elap = 0f;
        }

        public override void Execute()
        {
            if (elap >= 0.5f)
            {
                // if decoys are not yet deployed, change state
                if (!main.DecoyActive)
                {
                    fsm.SetState("Decoy");
                    return;
                }

                // if invisibility is not yet activated, change state
                if (!main.InvisActive)
                {
                    fsm.SetState("Invisible");
                    return;
                }
            }
            else elap += Time.deltaTime;

            Debug.Log("ESCAPE: attempting to escape...");

            // running away from the average central location of the alive target bots
            FindCentralPointOfTargets();
            RunAway();

            // if the nearest target is further than 25m, change state
            FindNearestTarget();
            if (Vector3.Distance(bot.position, nearestTarget) > 25)
            {
                fsm.SetState("Idle");
            }
        }

        public override void Exit()
        {
            main.ChangeToOriginal();
            Debug.Log("ESCAPE: escaped successfully");
        }

        // function to calculate and set the average central location of all alive target bots
        void FindCentralPointOfTargets()
        {
            targets = GameObject.FindGameObjectsWithTag("Target");

            float x = 0f;
            float z = 0f;

            foreach (GameObject target in targets)
            {
                // adding total x and z position of all alive target bots
                x += target.transform.position.x;
                z += target.transform.position.z;
            }

            // dividing to get average x and z position of all alive target bots
            x /= targets.Length;
            z /= targets.Length;

            // setting the average central location of the target bots
            centralLocation = new Vector3(x, bot.position.y, z);
        }

        // function to find the nearest target bot to the assassin bot
        void FindNearestTarget()
        {
            // setting the inital closest distance to the largest possible
            float closestDist = Mathf.Infinity;
            targets = GameObject.FindGameObjectsWithTag("Target");

            foreach (GameObject target in targets)
            {
                float dist = Vector3.Distance(bot.position, target.transform.position);

                // checking if the distance between this target bot and the assassin bot is the smallest
                if (dist < closestDist)
                {
                    // setting the cloesest target and the closest distance value
                    nearestTarget = target.transform.position;
                    closestDist = dist;
                }
            }
        }

        void RunAway()
        {
            // setting the target location to be slightly infront of the assassin bot
            // when it is facing away from the average central location of the target bots
            moveLocation = bot.position + (bot.position - centralLocation);

            // setting the target rotation
            lookRotation = Quaternion.LookRotation((moveLocation - bot.position), Vector3.up);

            // moving towards the target location
            bot.position =
                Vector3.MoveTowards(bot.position, moveLocation, main.Speed * Time.deltaTime);
            
            // smoothly rotating towards the target rotation
            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }
    }
}