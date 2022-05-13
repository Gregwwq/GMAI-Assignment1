using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssassinBot
{
    public class SelectState : FSMState<string>
    {
        const string Name = "Select Weapon";

        AssassinBotFSM main;

        int choice;
        float elap;

        public SelectState(FSM<string> _fsm, AssassinBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
        }

        public override void Enter()
        {
            Debug.Log("SELECT: choosing a weapon to use");

            elap = 0f;
            choice = 0;
        }

        public override void Execute()
        {
            if(elap >= 1f)
            {
                Proceed();
            }
            else elap += Time.deltaTime;
        }

        public override void Exit()
        {

        }

        void Proceed()
        {
            choice = Random.Range(0, 2);

            switch(choice)
            {
                case 0:
                    main.EquipSword();
                    fsm.SetState("Pursue");
                    Debug.Log("SELECT: sword equipped");
                    break;
                
                case 1:
                    main.EquipThrowingKnife();
                    fsm.SetState("Pursue");
                    Debug.Log("SELECT: throwing knives equipped");
                    break;
                
                case 2:
                    main.EquipSniper();
                    fsm.SetState("High Ground");
                    Debug.Log("SELECT: sniper equipped");
                    break;
            }
        }
    }
}