using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Sprint0.Commands;
using Zelda.Enums;

namespace Sprint0.Controllers
{
    public class GamePadController : PlayerController
    {
        private readonly Dictionary<GamePadButtonEnums, ICommand> buttonCommandMap;
        private readonly Dictionary<GamePadJoystickEnums, ICommand> joystickCommandMap;
        private readonly Dictionary<GamePadTriggerEnums, ICommand> triggerCommandMap;
        private readonly Dictionary<GamePadDPadEnums, ICommand> dPadCommandMap;
        private GamePadButtonEnums lastButton;
        private GamePadJoystickEnums lastJoystick;
        private GamePadTriggerEnums lastTrigger;
        private GamePadDPadEnums lastDPad;

        // Parameters of constructor are four dictionaries, one for buttons, joysticks, triggers, and DPad respectively:
        public GamePadController(Dictionary<GamePadButtonEnums, ICommand> buttonMap, Dictionary<GamePadJoystickEnums, ICommand> joystickMap, Dictionary<GamePadTriggerEnums, ICommand> triggerMap, Dictionary<GamePadDPadEnums, ICommand> dPadMap)
        {
            buttonCommandMap = buttonMap;
            joystickCommandMap = joystickMap;
            triggerCommandMap = triggerMap;
            dPadCommandMap = dPadMap;
            // Assign default values for lastInputs upon construction:
            lastButton = GamePadButtonEnums.None;
            lastJoystick = GamePadJoystickEnums.None;
            lastTrigger = GamePadTriggerEnums.None;
            lastDPad = GamePadDPadEnums.None;
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
 
