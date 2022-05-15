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
            if (elap >= 1f)
            {
                choice = Random.Range(0, 3);
                Proceed();
            }
            else elap += Time.deltaTime;
        }

        public override void Exit()
        {

        }

        void Proceed()
        {
            switch (choice)
            {
                case 0:
                    main.EquipSword();
                    Debug.Log("SELECT: sword equipped");
                    fsm.SetState("Pursue");
                    break;

                case 1:
                    if (main.ThrowingKnives < 1)
                    {
                        Debug.Log("SELECT: ran out of throwing knives, selecting another weapon");
                        choice = Random.Range(0, 2) == 0 ? 0 : 2;
                        Proceed();
                        return;
                    }

                    main.EquipThrowingKnife();
                    Debug.Log("SELECT: throwing knives equipped");
                    fsm.SetState("Pursue");
                    break;

                case 2:
                    if (main.SniperBullets < 1)
                    {
                        Debug.Log("SELECT: ran out of sniper bullets, selecting another weapon");
                        choice = Random.Range(0, 2);
                        Proceed();
                        return;
                    }

                    main.EquipSniper();
                    Debug.Log("SELECT: sniper equipped");
                    fsm.SetState("High Ground");
                    break;
            }
        }
    }
}