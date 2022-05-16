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
            // if the target bot is within 1m of the assassin bot and it is not invisible, catch the assassin bot
            if (Vector3.Distance(bot.position, assassin.position) < 1f && !assassinAlreadyInvis)
            {
                assassin.gameObject.GetComponent<AssassinBotFSM>().TriggerSuicide();
            }

            // checking if the assasin has already turned invisible
            if (assassin.gameObject.GetComponent<AssassinBotFSM>().InvisActive) assassinAlreadyInvis = true;

            // checking if there are still decoys left to chase and if the assassin is already invisible
            // if so, change state
            decoys = GameObject.FindGameObjectsWithTag("Decoy");
            if (assassinAlreadyInvis && decoys.Length <= 0) fsm.SetState("Wander");

            // if the assassin has already become invisible
            if (assassinAlreadyInvis)
            {
                // chase te nearest decoy
                GetNearestDecoy();
                MoveTowardsNearestDecoy();
            }
            else
            {
                // if not, chase the assassin
                MoveTowardsAssassin();
            }
        }

        public override void Exit()
        {

        }

        // function to find the nearest decoy
        void GetNearestDecoy()
        {
            // setting the inital closest distance to the largest possible
            float closestDist = Mathf.Infinity;

            foreach (GameObject decoy in decoys)
            {
                float dist = Vector3.Distance(bot.position, decoy.transform.position);

                // checking if the distance between this decoy and the target bot is the smallest
                if (dist < closestDist)
                {
                    // setting the cloesest decoy and the closest distance value
                    nearestDecoy = decoy.transform.position;
                    closestDist = dist;
                }
            }
        }

        // function to chase the assassin
        void MoveTowardsAssassin()
        {
            // setting the target rotation to face the assassin
            lookRotation = Quaternion.LookRotation((assassin.position - bot.position), Vector3.up);

            // moving towards the assassin
            bot.position =
                Vector3.MoveTowards(bot.position, assassin.position, main.Speed * Time.deltaTime);

            // smoothly rotating to face the target rotation
            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }

        // function to chase the nearest decoy
        void MoveTowardsNearestDecoy()
        {
            // setting the target rotation to face the nearest decoy
            lookRotation = Quaternion.LookRotation((nearestDecoy - bot.position), Vector3.up);

            // moving towards the nearest decoy
            bot.position =
                Vector3.MoveTowards(bot.position, nearestDecoy, main.Speed * Time.deltaTime);

            // smoothly rotating to face the target rotation
            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }
    }
}