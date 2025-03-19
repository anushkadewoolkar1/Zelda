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
        // Called once per frame to update sprites
        void Update(GameTime gameTime);

        // Draws sprite on the screen
        void Draw(SpriteBatch spriteBatch);

    }
}
