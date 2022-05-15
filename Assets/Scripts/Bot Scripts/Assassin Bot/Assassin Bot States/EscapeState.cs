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
                if (!main.DecoyActive)
                {
                    fsm.SetState("Decoy");
                    return;
                }

                if (!main.InvisActive)
                {
                    fsm.SetState("Invisible");
                    return;
                }
            }
            else elap += Time.deltaTime;

            Debug.Log("ESCAPE: attempting to escape...");

            FindCentralPointOfTargets();
            RunAway();

            FindNearestTarget();
            if (Vector3.Distance(bot.position, nearestTarget) >= 25)
            {
                fsm.SetState("Idle");
            }
        }

        public override void Exit()
        {
            main.ChangeToOriginal();
            Debug.Log("ESCAPE: escaped successfully");
        }

        void FindCentralPointOfTargets()
        {
            targets = GameObject.FindGameObjectsWithTag("Target");

            float x = 0f;
            float z = 0f;

            foreach (GameObject target in targets)
            {
                x += target.transform.position.x;
                z += target.transform.position.z;
            }

            x /= targets.Length;
            z /= targets.Length;

            centralLocation = new Vector3(x, bot.position.y, z);
        }

        void FindNearestTarget()
        {
            float closestDist = Mathf.Infinity;
            targets = GameObject.FindGameObjectsWithTag("Target");

            foreach (GameObject target in targets)
            {
                float dist = Vector3.Distance(bot.position, target.transform.position);
                if (dist < closestDist)
                {
                    nearestTarget = target.transform.position;
                    closestDist = dist;
                }
            }
        }

        void RunAway()
        {
            moveLocation = bot.position + (bot.position - centralLocation);

            lookRotation = Quaternion.LookRotation((moveLocation - bot.position), Vector3.up);

            bot.position =
                Vector3.MoveTowards(bot.position, moveLocation, main.Speed * Time.deltaTime);
            
            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }
    }
}