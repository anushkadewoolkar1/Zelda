using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Commands;
using Sprint0.Display;
using Zelda.Enums;


namespace Sprint0.Display
{
    // Interface for menus, extending the behavior of a display (PP):
    public abstract class IMenu : IDisplay
    {
        // Menu should have control over game state, to switch property currDisplay:
        protected Game1 gameCopy;

        // Called once per frame to update sprites
        public abstract void Update(GameTime gameTime);

        // Draws sprites on the screen
        public abstract void Draw(SpriteBatch spriteBatch);

        // Executes a state modification based on command definition:
        public void LoadCommand(ICommand command)
        {

        }

        // Needed to load other menus (Options -> Inventory), (Death -> Start), or (Options -> Level):
        public void SwitchDisplay(IDisplay display)
        {
            gameCopy.currDisplay = display;
        }

        public void ActivateLevelInputMap()
        {
            //gameCopy.SwitchInputMap();
        }
    }
}
