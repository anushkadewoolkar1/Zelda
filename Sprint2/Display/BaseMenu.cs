using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Commands;
using Sprint0.Display;
using Zelda.Enums;


namespace Sprint0.Display
{
    /*
     * Base concrete class for a menu, used to hold input functionality and common menu behavior
     * while specific subclasses will implement UI and specific menu functions (PP):
     */
    public class BaseMenu : IMenu
    {
        protected Dictionary<InputCommands, ICommand> inputCommandMap;
        protected IDisplay returnDisplay;
        
        // Should not be instantiated
        public BaseMenu(Game1 game, IDisplay defaultDisplay) {

        }

        // Called once per frame to update sprites
        public override void Update(GameTime gameTime)
        {

        }

        // Empty as sprite drawing is handled in subclasses, common menu textures may be put here but its unlikely they overlap:
        public override void Draw(SpriteBatch spriteBatch) { }

        // Executes a state modification based on command definition:
        public override void LoadCommand(ICommand command)
        {

        }

        // Holds the current state of level to be brought back later:
        public override void PauseLevel(Level level)
        {

        }

        public override void SwitchDisplay(IDisplay display)
        {
            gameCopy.currDisplay = display;
        }

        public Dictionary<InputCommands, ICommand> GetInputCommandMap()
        {
            // Create input to command dictionary
        }

        // Maybe we need this? idk yet: 
        // public abstract void UnpauseLevel(Level level);
    }
}