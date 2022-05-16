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
                // getting a random choice as an integer
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
                    // equipping the sword
                    main.EquipSword();
                    Debug.Log("SELECT: sword equipped");
                    fsm.SetState("Pursue");
                    break;

                case 1:
                    // checking if there are throwing knives left to use
                    if (main.ThrowingKnives < 1)
                    {
                        // if there isnt, choosing a random weapon from the other 2 options
                        Debug.Log("SELECT: ran out of throwing knives, selecting another weapon");
                        choice = Random.Range(0, 2) == 0 ? 0 : 2;
                        Proceed();
                        return;
                    }

                    // equipping the throwing knives
                    main.EquipThrowingKnife();
                    Debug.Log("SELECT: throwing knives equipped");
                    fsm.SetState("Pursue");
                    break;

                case 2:
                    // checking if there are sniper bullets left to use
                    if (main.SniperBullets < 1)
                    {
                        // if there isnt, choosing a random weapon from the other 2 options
                        Debug.Log("SELECT: ran out of sniper bullets, selecting another weapon");
                        choice = Random.Range(0, 2);
                        Proceed();
                        return;
                    }

                    // equipping the sniper
                    main.EquipSniper();
                    Debug.Log("SELECT: sniper equipped");
                    fsm.SetState("High Ground");
                    break;
            }
        }
    }
}