using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{
    // Interface for game sprites
    public interface ISprite
    {
        // Called once per frame to update sprites
        void Update(GameTime gameTime);
        
        //Only for Boomerang

        void Update(GameTime gameTime, Link link);

        // Draws sprite on the screen
        void Draw(SpriteBatch spriteBatch);

        // Draws sprite on the screen, NOW requires position to be passed, used for LinkSprite
        void Draw(SpriteBatch spriteBatch, Vector2 position);
    }
}
