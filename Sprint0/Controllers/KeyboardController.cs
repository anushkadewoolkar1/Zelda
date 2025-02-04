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
}
