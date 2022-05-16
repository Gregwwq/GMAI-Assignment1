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
            // setting the target bot to have the dead tag
            main.gameObject.tag = "DeadTarget";

            main.ChangeColor(Color.black);

            // toppling the target bot
            ToppleToTheSide();
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {

        }
        
        // function to topple the target bot to one side
        void ToppleToTheSide()
        {
            // getting a random direction to topple, left or right
            // and toppling in that respective directions
            side = Random.Range(0, 2);
            switch(side)
            {
                case 0:
                    bot.eulerAngles =  bot.eulerAngles + (90f * Vector3.forward);
                    break;
                
                case 1:
                    bot.eulerAngles =  bot.eulerAngles - (90f * Vector3.forward);;
                    break;
            }

            // alleviating the target bot abit to prevent it from clipping into the ground
            bot.position = new Vector3(bot.position.x, 0.4f, bot.position.z);
        }
    }
}