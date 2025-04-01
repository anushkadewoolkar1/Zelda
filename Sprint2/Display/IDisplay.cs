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

        // Can swap from level to menu, menu to level, menu to menu, etc.:
        void SwitchDisplay(IDisplay newDisplay);

        // Switches user input set from player controls to menu controls, and vice versa:
        void SwitchesUISet();
    }
}
