using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Sprint0.Commands;

namespace Sprint0.Controllers
{
    public class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> _keyCommandMap;

        public KeyboardController(Dictionary<Keys, ICommand> keyCommandMap)
        {
            _keyCommandMap = keyCommandMap;
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            Keys[] pressedKeys = state.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                // if key in map execute
                if (_keyCommandMap.ContainsKey(key))
                {
                    _keyCommandMap[key].Execute();
                }
            }
        }
    }

    public class MouseController : IController
    {
        private readonly ICommand _nonMovingNonAnimatedCommand;
        private readonly ICommand _nonMovingAnimatedCommand;
        private readonly ICommand _movingNonAnimatedCommand;
        private readonly ICommand _movingAnimatedCommand;
        private readonly ICommand _quitCommand;

        private int _windowWidth;
        private int _windowHeight;

        public MouseController(
            ICommand nonMovingNonAnimatedCommand, 
            ICommand nonMovingAnimatedCommand, 
            ICommand movingNonAnimatedCommand, 
            ICommand movingAnimatedCommand, 
            ICommand quitCommand, 
            int windowWidth, int windowHeight)
        {
            _nonMovingNonAnimatedCommand = nonMovingNonAnimatedCommand;
            _nonMovingAnimatedCommand = nonMovingAnimatedCommand;
            _movingNonAnimatedCommand = movingNonAnimatedCommand;
            _movingAnimatedCommand = movingAnimatedCommand;
            _quitCommand = quitCommand;
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public void Update()
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
                    _nonMovingNonAnimatedCommand.Execute();
                }
                // top right
                else if (topHalf && !leftHalf)
                {
                    _nonMovingAnimatedCommand.Execute();
                }
                // bottom left
                else if (!topHalf && leftHalf)
                {
                    _movingNonAnimatedCommand.Execute();
                }
                // bottom right
                else
                {
                    _movingAnimatedCommand.Execute();
                }
            }
        }
    }
}
