using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using MainGame.Commands;
using MainGame.Display;

namespace MainGame.Controllers
{
    public class MouseController : DebugController
    {
        private readonly ICommand _quitCommand;

        private int _windowWidth;
        private int _windowHeight;

        public MouseController(
            int windowWidth, int windowHeight)
        {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public override void Update(LevelManager level)
        {
            MouseState state = Mouse.GetState();

            // If right button pressed, quit
            if (state.RightButton == ButtonState.Pressed)
            {
                //commented this out cuz it its null since _quitcommand is not instantiated and we cant here since we dont have game in this file. Just use escape.
                //_quitCommand.Execute(); 
                return;
            }

            // If left button pressed, check which quarter the mouse is in
            if (state.LeftButton == ButtonState.Pressed)
            {
                int mouseX = state.X;
                int mouseY = state.Y;

                bool topHalf = mouseY < _windowHeight / 2;
                bool leftHalf = mouseX < _windowWidth / 2;

                level.moveLink = false;
                // top left
                /*
                if (topHalf && leftHalf)
                {
                    
                    level.LoadRoom(level.currentRoom[0], level.currentRoom[1] - 1);
                }
                // top right
                else if (topHalf && !leftHalf)
                {
                    level.LoadRoom(level.currentRoom[0] + 1, level.currentRoom[1]);
                }
                // bottom left
                else if (!topHalf && leftHalf)
                {
                    level.LoadRoom(level.currentRoom[0] - 1, level.currentRoom[1]);
                }
                // bottom right
                else
                {
                    level.LoadRoom(level.currentRoom[0], level.currentRoom[1] + 1);
                }
                level.moveLink = true;
                

                System.Diagnostics.Debug.WriteLine(level.currentRoom[0].ToString() + "||"
                    + level.currentRoom[1].ToString());
                */
            }
        }
    }
}