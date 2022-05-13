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
        Vector3 targetLocation, currentLocation;
        Quaternion lookRotation;

        public WanderState(FSM<string> _fsm, TargetBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            targetLocation = main.gameObject.transform.position;
        }

        public override void Execute()
        {
            Move();

            if (Vector3.Distance(currentLocation, targetLocation) < 0.001f)
            {
                UpdateNewMovement();
            }
        }

        public override void Exit()
        {

        }

        void UpdateNewMovement()
        {
            minX = currentLocation.x - 8;
            maxX = currentLocation.x + 8;
            minZ = currentLocation.z - 8;
            maxZ = currentLocation.z + 8;

            targetLocation = new Vector3(Random.Range(minX, maxX), currentLocation.y, Random.Range(minZ, maxZ));

            lookRotation = Quaternion.LookRotation((targetLocation - currentLocation), Vector3.up);
        }

        void Move()
        {
            currentLocation = main.gameObject.transform.position;

            main.gameObject.transform.position =
                Vector3.MoveTowards(currentLocation, targetLocation, main.Speed * Time.deltaTime);
        
            main.gameObject.transform.rotation = Quaternion.RotateTowards(main.gameObject.transform.rotation, lookRotation, 200 * Time.deltaTime);
        }
    }
}