using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using MainGame.Commands;
using Zelda.Enums;

namespace MainGame.Controllers
{
    public class GamePadController : PlayerController
    {
        private const float HALF_SQRT_TWO = (float).7071;
        public GamePadController(Dictionary<UserInputs, ICommand> levelMap, Dictionary<UserInputs, ICommand> menuMap)
        {
            SetCommandMaps(levelMap, menuMap);
        }

        public override void Update()
        {

            GamePadState state = GamePad.GetState(0);
            if (state.Buttons.A.Equals(ButtonState.Pressed)) ExecuteActionInput(UserInputs.AttackMelee);
            if (state.Buttons.B.Equals(ButtonState.Pressed)) ExecuteActionInput(UserInputs.UseItem);
            if (state.Buttons.Start.Equals(ButtonState.Pressed)) ExecuteActionInput(UserInputs.ToggleInventory);
            if (state.Buttons.BigButton.Equals(ButtonState.Pressed)) ExecuteActionInput(UserInputs.ToggleInventory);

            // Declare variables to make remaining code more simple to read:
            float joystickX = state.ThumbSticks.Left.X, joystickY = state.ThumbSticks.Left.Y;

            // If |X or Y position| of joystick is greater than sqrt(2)/2, then it must be biased in that direction:
            if (joystickX < -HALF_SQRT_TWO) ExecuteDirectionInput(UserInputs.MoveLeft);
            else if (joystickX >= HALF_SQRT_TWO) ExecuteDirectionInput(UserInputs.MoveRight);
            else if (joystickY < -HALF_SQRT_TWO) ExecuteDirectionInput(UserInputs.MoveDown);
            else if (joystickX >= HALF_SQRT_TWO) ExecuteDirectionInput(UserInputs.MoveUp);

            // Update DPad State:
            if (state.DPad.Down.Equals(ButtonState.Pressed)) ExecuteDirectionInput(UserInputs.MoveDown);
            else if (state.DPad.Up.Equals(ButtonState.Pressed)) ExecuteDirectionInput(UserInputs.MoveUp);
            else if (state.DPad.Left.Equals(ButtonState.Pressed)) ExecuteDirectionInput(UserInputs.MoveLeft);
            else if (state.DPad.Right.Equals(ButtonState.Pressed)) ExecuteDirectionInput(UserInputs.MoveRight);

            UpdateFlags();
        }
    }
}
 
