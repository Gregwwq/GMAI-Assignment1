using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TargetBot
{
    public class WanderState : FSMState<string>
    {
        const string Name = "Wander";

        TargetBotFSM main;

        float minX, maxX, minZ, maxZ;
        Transform bot;
        Vector3 targetLocation;
        Quaternion lookRotation;

        public WanderState(FSM<string> _fsm, TargetBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            targetLocation = bot.position;
            main.ChangeColor(Color.green);
        }

        public override void Execute()
        {
            // moving the target bot to the target location
            Move();

            // if the target bot has reached its target location, generate a new target location
            if (Vector3.Distance(bot.position, targetLocation) < 0.001f)
            {
                UpdateNewMovement();
            }
        }

        public override void Exit()
        {

        }

        // funciton to move the target bot to its destination
        void Move()
        {
            // moving towards the set target location
            bot.position =
                Vector3.MoveTowards(bot.position, targetLocation, main.Speed * Time.deltaTime);

            // smoothly rotating towards the target rotation
            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }

        void UpdateNewMovement()
        {
            // setting the boundaries of the targt location
            minX = bot.position.x - 8;
            maxX = bot.position.x + 8;
            minZ = bot.position.z - 8;
            maxZ = bot.position.z + 8;

            // setting the random target location within the boundaries
            targetLocation = new Vector3(Random.Range(minX, maxX), bot.position.y, Random.Range(minZ, maxZ));

            // setting the target rotating to face te target location
            lookRotation = Quaternion.LookRotation((targetLocation - bot.position), Vector3.up);
        }
    }
}