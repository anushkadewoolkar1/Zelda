using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Level
{
    // Interface for game sprites
    public interface ILevel
    {
        // Called once per frame to update sprites
        void Update(GameTime gameTime);

        // Draws sprite on the screen
        void Draw(SpriteBatch spriteBatch);

        //Load next room
        void LoadRoom(int xCoordinate, int yCoordinate);
    }
}
