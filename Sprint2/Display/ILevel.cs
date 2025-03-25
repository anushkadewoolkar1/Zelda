using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.CollisionHandling;
using Sprint0.Display;

namespace Sprint0.Display
{
    // Interface for game sprites
    public abstract class ILevel : IDisplay
    {
        protected Game gameCopy;

        // Called once per frame to update sprites
        public abstract void Update( GameTime gameTime);

        // Draws sprite on the screen
        public abstract void Draw(SpriteBatch spriteBatch);

        //Load next room
        public abstract void LoadRoom(int xCoordinate, int yCoordinate);

        //Switches from level to menu:
        public abstract void SwitchDisplay(IDisplay targetDisplay);
    }
}
