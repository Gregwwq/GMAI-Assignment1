using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class DecoyState : FSMState<string>
    {
        const string Name = "Decoy";

        AssassinBotFSM main;

        GameObject decoyPrefab;

        float elap;
        int numOfDecoys;

        public DecoyState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            decoyPrefab = (GameObject) Resources.Load("Prefabs/Decoy", typeof(GameObject));
        }

        public override void Enter()
        {
            elap = 0f;
            numOfDecoys = 0;

            Debug.Log("DECOY: deploying 6 decoys");
        }

        public override void Execute()
        {
            if (numOfDecoys >= 6)
            {
                fsm.SetState("Escape");
                return;
            }

            if (elap >= 0.2f)
            {
                DeployDecoy();
            }
            else elap += Time.deltaTime;
        }

        public override void Exit()
        {
            main.UseDecoy();

            Debug.Log("DECOY: all decoys have been deployed");
            Debug.Log("DECOY: " + main.DecoyCount + " decoy skill charges left");
        }

        void DeployDecoy()
        {
            Debug.Log("DECOY: deploying a decoy");
            GameObject.Instantiate(decoyPrefab, main.gameObject.transform.position, Quaternion.identity);
            numOfDecoys++;
            elap = 0f;
        }
    }
}