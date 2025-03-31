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
        
        // Used by subclasses:
        public BaseMenu(Game1 game, IDisplay defaultDisplay) {
            gameCopy = game;
            returnDisplay = defaultDisplay;
        }

        // Called once per frame to update sprites
        public override void Update(GameTime gameTime)
        {

        }

        // Draws black menu background:
        public void DrawBackground() { 
        
        }

    }
}