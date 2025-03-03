using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Sprint0.Commands;
using Sprint0.ILevel;

namespace Sprint0.Controllers
{
    public class MouseController : IController
    {
        private readonly ICommand _nonMovingNonAnimatedCommand;
        private readonly ICommand _nonMovingAnimatedCommand;
        private readonly ICommand _movingNonAnimatedCommand;
        private readonly ICommand _movingAnimatedCommand;
        private readonly ICommand _quitCommand;

        private int _windowWidth;
        private int _windowHeight;

        public MouseController(/*
            ICommand nonMovingNonAnimatedCommand,
            ICommand nonMovingAnimatedCommand,
            ICommand movingNonAnimatedCommand,
            ICommand movingAnimatedCommand,
            ICommand quitCommand,*/
            int windowWidth, int windowHeight)
        {
            /*_nonMovingNonAnimatedCommand = nonMovingNonAnimatedCommand;
            _nonMovingAnimatedCommand = nonMovingAnimatedCommand;
            _movingNonAnimatedCommand = movingNonAnimatedCommand;
            _movingAnimatedCommand = movingAnimatedCommand;
            _quitCommand = quitCommand;*/
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public void Update()
        {
            //no-op
        }

        public void Update(Level level)
        {
            MouseState state = Mouse.GetState();

            // If right button pressed, quit
            if (state.RightButton == ButtonState.Pressed)
            {
                _quitCommand.Execute();
                return;
            }

            // If left button pressed, check which quarter the mouse is in
            if (state.LeftButton == ButtonState.Pressed)
            {
                int mouseX = state.X;
                int mouseY = state.Y;

                bool topHalf = mouseY < _windowHeight / 2;
                bool leftHalf = mouseX < _windowWidth / 2;


                

                // top left
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

                System.Diagnostics.Debug.WriteLine(level.currentRoom[0].ToString() + "||"
                    + level.currentRoom[1].ToString());
            }
        }
    }
}