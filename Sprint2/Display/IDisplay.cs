using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Display;

namespace Sprint0.Display
{
    
    public interface IDisplay
    {
        // Called once per frame to update the state of sprites in the display
        void Update(GameTime gameTime);

        // Draws background texture and SpriteBatch sprites on the screen
        void Draw(SpriteBatch spriteBatch);

        // Switches Game1 property from the current display object to the IDisplay argument:
        void SwitchDisplay(IDisplay display);

    }
}
