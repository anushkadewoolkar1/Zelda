using System;
using System.Collections.Generic;
using System.Data.Common;
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
        private readonly Dictionary<GamePadDPadEnums, ICommand> dPadCommandMap;
        private GamePadButtonEnums lastButton;
        private GamePadJoystickEnums lastJoystick;
        private GamePadDPadEnums lastDPad;

        //Used as diagonal value of joystick:
        private const float HALF_SQRT_TWO = (float).7071;

        // Parameters of constructor are three dictionaries, one for buttons, joysticks, and DPad respectively:
        public GamePadController(Dictionary<GamePadButtonEnums, ICommand> buttonMap, Dictionary<GamePadJoystickEnums, ICommand> joystickMap, Dictionary<GamePadDPadEnums, ICommand> dPadMap)
        {
            buttonCommandMap = buttonMap;
            joystickCommandMap = joystickMap;
            dPadCommandMap = dPadMap;

            // Assign default values for lastInputs upon construction:
            lastButton = GamePadButtonEnums.None;
            lastJoystick = GamePadJoystickEnums.None;
            lastDPad = GamePadDPadEnums.None;
        }

        public override void Update()
        {
            bool anyButtonPressed = false, anyJoystickMoved = false, anyDPadPressed = false;
            GamePadState state = GamePad.GetState(0);

            // Using if statements as I am unable to iterate through the GamePadState structs, we can talk later about a potential alternative:
            if (state.Buttons.A.Equals(ButtonState.Pressed))
            {
                if (lastButton != GamePadButtonEnums.DownBtn)
                {
                    buttonCommandMap[GamePadButtonEnums.DownBtn].Execute();
                    lastButton = GamePadButtonEnums.DownBtn;
                }
                anyButtonPressed = true;
            }
            if (state.Buttons.B.Equals(ButtonState.Pressed))
            {
               if (lastButton != GamePadButtonEnums.RightBtn)
               {
                    buttonCommandMap[GamePadButtonEnums.RightBtn].Execute();
                    lastButton = GamePadButtonEnums.RightBtn;
                }
                anyButtonPressed = true;
            }


            /*
             * Uncomment once pause menu and options menu are implemented:
             * 
            if (state.Buttons.Start.Equals(ButtonState.Pressed))
            {
               if (lastButton != GamePadButtonEnums.Start)
               {
                    buttonCommandMap[GamePadButtonEnums.Start].Execute();
                    lastButton = GamePadButtonEnums.Start;
                }
                anyButtonPressed = true;
            }
            if (state.Buttons.Select.Equals(ButtonState.Pressed))
            {
               if (lastButton != GamePadButtonEnums.Select)
               {
                    buttonCommandMap[GamePadButtonEnums.Select].Execute();
                    lastButton = GamePadButtonEnums.Select;
                }
                anyButtonPressed = true;
            }
            */

            // If no buttons are pressed, nothing should happen:
            if (!anyButtonPressed)
            {
                lastButton = GamePadButtonEnums.None;
            }

            // Declare variables to make remaining code more simple to read:
            float joystickX = state.ThumbSticks.Left.X, joystickY = state.ThumbSticks.Left.Y;

            // If |X or Y position| of joystick is greater than sqrt(2)/2, then it must be biased in that direction:
            if (joystickX < (-1 * HALF_SQRT_TWO))
            {
                if (lastJoystick != GamePadJoystickEnums.LeftLJS)
                {
                    joystickCommandMap[GamePadJoystickEnums.LeftLJS].Execute();
                    lastJoystick = GamePadJoystickEnums.LeftLJS;
                }
                anyJoystickMoved = true;
            } else if (joystickX >= HALF_SQRT_TWO)
            {
                if (lastJoystick != GamePadJoystickEnums.RightLJS)
                {
                    joystickCommandMap[GamePadJoystickEnums.RightLJS].Execute();
                    lastJoystick = GamePadJoystickEnums.RightLJS;
                }
                anyJoystickMoved = true;
            } else if (joystickY < (-1 * HALF_SQRT_TWO))
            {
                if (lastJoystick != GamePadJoystickEnums.DownLJS)
                {
                    joystickCommandMap[GamePadJoystickEnums.DownLJS].Execute();
                    lastJoystick = GamePadJoystickEnums.DownLJS;
                }
                anyJoystickMoved = true;
            } else if (joystickX >= HALF_SQRT_TWO)
            {
                if (lastJoystick != GamePadJoystickEnums.UpLJS)
                {
                    joystickCommandMap[GamePadJoystickEnums.UpLJS].Execute();
                    lastJoystick = GamePadJoystickEnums.UpLJS;
                }
                anyJoystickMoved = true;
            } else
            {
                lastJoystick = GamePadJoystickEnums.None;
            }


            // Update DPad State:
            if (state.DPad.Down.Equals(ButtonState.Pressed))
            {
                if (lastDPad != GamePadDPadEnums.Down)
                {
                    dPadCommandMap[GamePadDPadEnums.Down].Execute();
                    lastDPad = GamePadDPadEnums.Down;
                }
                anyDPadPressed = true;
            }
            if (state.DPad.Up.Equals(ButtonState.Pressed))
            {
                if (lastDPad != GamePadDPadEnums.Up)
                {
                    dPadCommandMap[GamePadDPadEnums.Up].Execute();
                    lastDPad = GamePadDPadEnums.Up;
                }
                anyDPadPressed = true;
            }
            if (state.DPad.Left.Equals(ButtonState.Pressed))
            {
                if (lastDPad != GamePadDPadEnums.Left)
                {
                    dPadCommandMap[GamePadDPadEnums.Left].Execute();
                    lastDPad = GamePadDPadEnums.Left;
                }
                anyDPadPressed = true;
            }
            if (state.DPad.Right.Equals(ButtonState.Pressed))
            {
                if (lastDPad != GamePadDPadEnums.Right)
                {
                    dPadCommandMap[GamePadDPadEnums.Right].Execute();
                    lastDPad = GamePadDPadEnums.Right;
                }
                anyDPadPressed = true;
            }

            // If neither joystick or DPad is pressed, reset Link to idle state:
            if (!anyJoystickMoved && !anyDPadPressed)
            {
                dPadCommandMap[GamePadDPadEnums.None].Execute();
            }
        }
    }
}
 
