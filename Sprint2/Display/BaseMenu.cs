using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.Commands;
using MainGame.Display;
using Zelda.Enums;


namespace MainGame.Display
{
    // Interface for menus, extending the behavior of a display (PP):
    public abstract class BaseMenu : IDisplay
    {
        public readonly GameState identity;

        // Called once per frame to update sprites
        public abstract void Update(GameTime gameTime);

        // Draws sprites on the screen
        public abstract void Draw(SpriteBatch spriteBatch);

        // Executes a state modification based on command definition:
        public abstract void LoadCommand(ICommand command);

        public abstract void UpdateGameState(GameState gameStates);
    }
}