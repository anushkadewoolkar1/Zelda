using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using MainGame.Commands;
using MainGame.Display;
using Zelda.Enums;

namespace MainGame.Controllers
{
    public abstract class PlayerController : IController
    {
        protected Dictionary<UserInputs, ICommand> levelCommandMap { private get; set; }
        protected Dictionary<UserInputs, ICommand> menuCommandMap { private get; set; }
        protected bool AnyActionInput = false;
        protected bool AnyMoveInput = false;

        private UserInputs lastAction = UserInputs.None;
        private UserInputs lastMovement = UserInputs.None;
        public GameState gameStateCopy { get; set; }

        public abstract void Update();


        // Parent methods, to be utilized by subclass controller objects:

        public void SetCommandMaps(Dictionary<UserInputs, ICommand> levelMap, Dictionary<UserInputs, ICommand> menuMap)
        {

            if (levelMap != null) this.levelCommandMap = levelMap;
            if (menuMap != null) this.menuCommandMap = menuMap;

        }

        public void ExecuteActionInput(UserInputs input)
        {
            //Saving this piece of code for later(PP)
            //
            // Don't repeat held inputs
            if (input != lastAction)
                {
                    // If game is being played, use game control set, otherwise, use menu control set:
                    if (gameStateCopy == GameState.Playing) levelCommandMap[input].Execute();
                    else menuCommandMap[input].Execute();
                }


            if (lastAction != input) levelCommandMap[input].Execute();

            // Update flags:
            AnyActionInput = true;
            lastAction = input;
        }

        public void ExecuteDirectionInput(UserInputs input)
        {
            /* Saving this piece for later too (PP)
             * 
             * If game is being played, use game control set, otherwise, use menu control set: 
             * if (gameStateCopy == GameState.Playing) menuCommandMap[input].Execute();
             * else menuCommandMap[input].Execute();
            */

            levelCommandMap[input].Execute();

            // Update flag:
            AnyMoveInput = true;
            lastMovement = input;
        }

        public void ExecuteNoInput()
        {
            levelCommandMap[UserInputs.None].Execute();
        }

        public void UpdateFlags()
        {
            if (!AnyActionInput) lastAction = UserInputs.None;
            if (!AnyMoveInput) lastMovement = UserInputs.None;
            AnyActionInput = false;
            AnyMoveInput = false;
        }
    }
}
