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
        }

        public override void Execute()
        {
            Move();

            if (Vector3.Distance(bot.position, targetLocation) < 0.001f)
            {
                UpdateNewMovement();
            }
        }

        public override void Exit()
        {

        }

        void UpdateNewMovement()
        {
            minX = bot.position.x - 8;
            maxX = bot.position.x + 8;
            minZ = bot.position.z - 8;
            maxZ = bot.position.z + 8;

            targetLocation = new Vector3(Random.Range(minX, maxX), bot.position.y, Random.Range(minZ, maxZ));

            lookRotation = Quaternion.LookRotation((targetLocation - bot.position), Vector3.up);
        }

        void Move()
        {
            bot.position =
                Vector3.MoveTowards(bot.position, targetLocation, main.Speed * Time.deltaTime);

            bot.rotation =
                Quaternion.RotateTowards(bot.rotation, lookRotation, main.RotateSpeed * Time.deltaTime);
        }
    }
}