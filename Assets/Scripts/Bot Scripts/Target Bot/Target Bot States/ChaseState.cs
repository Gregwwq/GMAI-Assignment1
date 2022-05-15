using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TargetBot
{
    public class ChaseState : FSMState<string>
    {
        const string Name = "Chase";

        TargetBotFSM main;

        Transform bot, assassin;
        GameObject[] decoys;

        Vector3 nearestDecoy;
        Quaternion lookRotation;

        bool assassinAlreadyInvis;

        public ChaseState(FSM<string> _fsm, TargetBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            assassinAlreadyInvis = false;
            assassin = GameObject.Find("AssassinBot").transform;
            main.ChangeColor(Color.blue);
        }

        public override void Execute()
        {
            if (Vector3.Distance(bot.position, assassin.position) < 1f)
            {
                assassin.gameObject.GetComponent<AssassinBotFSM>().TriggerSuicide();
            }

            if (assassin.gameObject.GetComponent<AssassinBotFSM>().InvisActive) assassinAlreadyInvis = true;

            decoys = GameObject.FindGameObjectsWithTag("Decoy");
            if (assassinAlreadyInvis && decoys.Length <= 0) fsm.SetState("Wander");

            if(assassinAlreadyInvis)
            {
                GetNearestDecoy();
                MoveTowardsNearestDecoy();
            }
            else
            {
                MoveTowardsAssassin();
            }
        }

        public override void Exit()
        {

        }

        void GetNearestDecoy()
        {
            float closestDist = Mathf.Infinity;

            foreach (GameObject decoy in decoys)
            {
                float dist = Vector3.Distance(bot.position, decoy.transform.position);
                if (dist < closestDist)
                {
                    nearestDecoy = decoy.transform.position;
                    closestDist = dist;
                }
            }
        }

        void MoveTowardsAssassin()
        {
            lookRotation = Quaternion.LookRotation((assassin.position - bot.position), Vector3.up);

            bot.position =
                Vector3.MoveTowards(bot.position, assassin.position, main.Speed * Time.deltaTime);

            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }

        void MoveTowardsNearestDecoy()
        {
            lookRotation = Quaternion.LookRotation((nearestDecoy - bot.position), Vector3.up);

            bot.position =
                Vector3.MoveTowards(bot.position, nearestDecoy, main.Speed * Time.deltaTime);

            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }
    }
}