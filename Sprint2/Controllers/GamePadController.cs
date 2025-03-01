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
        private readonly Dictionary<GamePadInputs, ICommand> inputCommandMap;
        private GamePadInputs lastInput;
        private bool inputOnLastFrame;

        public GamePadController(Dictionary<GamePadState, ICommand> inputToCommandMap)
        {
            inputCommandMap = inputToCommandMap;
            // Assign default value for lastInput upon construction:
            lastInput = GamePadInputs.None;
            inputOnLastFrame = false;
        }

        public void Update()
        {
            bool inputTriggered = false;

            GamePadState state = GamePad.GetState();

            // Current Protocol for saving last input is DPad<Trigger<Button<Joysticks (PP):

            foreach (var DPadButton in state.DPad)
            {
                if (DPadButton.Pressed)
                {
                    inputCommandMap[DPadButton].Execute();
                    inputTriggered = true;
                    lastInput = DPadButton;
                }
            }

            foreach (var trigger in state.Triggers)
            {
                if (trigger > 0.5)
                {
                    inputCommandMap[trigger].Execute();
                    inputTriggered = true;
                    lastInput = trigger;
                }
            }

            foreach (var button in state.Buttons)
            {
                if (button.Pressed)
                {
                    inputCommandMap[button].Execute();
                    inputTriggered = true;
                    lastInput = button;
                }
            }

            foreach (var stick in state.ThumbSticks)
            {
                if (stick.x > 0.5)
                {
                    
                } else if (stick.x < -0.5) {
                    inputCommandMap[state.]
                }
            }

            if (!inputTriggered)
            {
                lastInput = GamePadInputs.None;
            } 

            inputOnLastFrame = inputTriggered;
        }
    }
}
