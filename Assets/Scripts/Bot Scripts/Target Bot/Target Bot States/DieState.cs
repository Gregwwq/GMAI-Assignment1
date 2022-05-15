using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TargetBot
{
    public class DieState : FSMState<string>
    {
        const string Name = "Die";

        TargetBotFSM main;

        Transform bot;
 
        int side;

        public DieState(FSM<string> _fsm, TargetBotFSM _main) : base(_fsm, Name)
        {
            main = _main;
            bot = main.gameObject.transform;
        }

        public override void Enter()
        {
            main.gameObject.tag = "DeadTarget";

            main.ChangeColor(Color.black);

            ToppleToTheSide();
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {

        }
        
        void ToppleToTheSide()
        {
            side = Random.Range(0, 1);
            switch(side)
            {
                case 0:
                    bot.rotation =  Quaternion.Euler(0, 0, 90);
                    break;
                
                case 1:
                    bot.rotation =  Quaternion.Euler(0, 0, -90);
                    break;
            }

            bot.position = new Vector3(bot.position.x, 0.4f, bot.position.z);
        }
    }
}