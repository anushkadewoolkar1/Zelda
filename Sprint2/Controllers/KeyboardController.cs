using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using MainGame.Commands;
using MainGame.Display;
using Zelda.Enums;

namespace MainGame.Controllers
{
    public class KeyboardController : PlayerController
    {
        private readonly Dictionary<Keys, UserInputs> keyCommandMap;
        private HashSet<UserInputs> directionInputs;
        public KeyboardController(Dictionary<UserInputs, ICommand> levelCommandMap, Dictionary<UserInputs, ICommand> menuCommandMap)
        {
            keyCommandMap = GenerateKeyToInputMap();
            SetCommandMaps(levelCommandMap, menuCommandMap);

            // Set up direction HashMap:
            directionInputs = new HashSet<UserInputs> { UserInputs.MoveUp, UserInputs.MoveDown, UserInputs.MoveLeft, UserInputs.MoveRight, UserInputs.RaiseVolume, UserInputs.LowerVolume };
        }

        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();

            Keys[] pressedKeys = state.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                // if key in map execute
                if (keyCommandMap.ContainsKey(key))
                {
                    UserInputs input = keyCommandMap[key];
                    if (directionInputs.Contains(input)) ExecuteDirectionInput(input);
                    else ExecuteActionInput(input);
                }
            }

            if (pressedKeys.Length == 0) ExecuteNoInput();

            UpdateFlags();
        }

        public Dictionary<Keys, UserInputs> GenerateKeyToInputMap()
        {
            return new Dictionary<Keys, UserInputs>
            {
                { Keys.None, UserInputs.None },

                //'Z' -> Player Attack:
                { Keys.Z, UserInputs.AttackMelee },

                //'N' -> Player Attack:
                { Keys.N, UserInputs.UseItem },

                //'W' -> Player Walk Up:
                { Keys.W, UserInputs.MoveUp },

                //'Up Arrow' -> Player Walk Up:
                { Keys.Up, UserInputs.MoveUp },

                //'A' -> Player Walk Left:
                { Keys.A, UserInputs.MoveLeft },

                //'Left Arrow' -> Player Walk Left:
                { Keys.Left, UserInputs.MoveLeft },

                //'S' -> Player Walk Down:
                { Keys.S, UserInputs.MoveDown },

                //'Down Arrow' -> Player Walk Down:
                { Keys.Down, UserInputs.MoveDown },

                //'D' -> Player Walk Right:
                { Keys.D, UserInputs.MoveRight },

                //'Right Arrow' -> Player Walk Right:
                { Keys.Right, UserInputs.MoveRight },

                //'D4' -> Player Use Sword:
                { Keys.D4, UserInputs.AttackMelee },

                { Keys.Enter, UserInputs.NewGame },

                { Keys.P, UserInputs.ToggleMute },

                { Keys.K, UserInputs.ToggleInventory },

                { Keys.L, UserInputs.ToggleOptions },

                { Keys.F, UserInputs.ToggleFullscreen },

                { Keys.OemPlus, UserInputs.RaiseVolume },

                { Keys.R, UserInputs.ResetLevel },

                { Keys.Tab, UserInputs.CycleItem },
                { Keys.V, UserInputs.ToggleStats }


            };
        }
    }
}
