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
    public class KeyboardController : PlayerController
    {
        private readonly Dictionary<Keys, ICommand> _keyCommandMap;
        // Add instance variable to store last input (with a default value for no input): (PP)
        private Keys lastInput;
        private bool lastKeyIdle = false;

        public KeyboardController(Dictionary<Keys, ICommand> keyCommandMap)
        {
            _keyCommandMap = keyCommandMap;
            // Assign default value for lastInput upon construction:
            lastInput = Keys.None;
        }

        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();

            Keys[] pressedKeys = state.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                // if key in map execute
                if (_keyCommandMap.ContainsKey(key))
                {
                    if (key != lastInput)
                    {
                        _keyCommandMap[key].Execute();
                    }
                    // Update lastInput if input is valid command: (PP)
                    lastInput = key;
                }
            }

            if (pressedKeys.Length > 0)
            {
                lastKeyIdle = false;
            }


            // If no input is given, reset lastInput to Keys.None (default state), and return Link to default idle state: (PP)
            if (pressedKeys.Length == 0)
            {
                lastInput = Keys.None;
                if (lastKeyIdle == false)
                {
                    _keyCommandMap[Keys.None].Execute();
                    lastKeyIdle = true;
                }
            }
        }
    }
}
