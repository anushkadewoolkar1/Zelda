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
        // Add instance variable to store last input (with a default value for no input): (PP)
        private Keys lastInput;

        public KeyboardController(Dictionary<Keys, ICommand> keyCommandMap)
        {
            _keyCommandMap = keyCommandMap;
            // Assign default value for lastInput upon construction:
            lastInput = Keys.None;
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            Keys[] pressedKeys = state.GetPressedKeys();

            // If no input is given, reset lastInput to its default: (PP)
            if (pressedKeys.Length == 0)
            {
                lastInput = Keys.None;
            }

            foreach (Keys key in pressedKeys)
            {
                // if key in map execute
                if (_keyCommandMap.ContainsKey(key))
                {
                    if (key != lastKey)
                    {
                        _keyCommandMap[key].Execute();
                    }
                    // Update lastInput if input is valid command: (PP)
                    lastInput = key;
                }
            }
        }
    }
}
