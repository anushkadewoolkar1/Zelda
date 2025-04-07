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
    public abstract class IMenu : IDisplay
    {
        private Texture2D background;
        private Texture2D cursor;
        protected int cursorIndex = 0;
        protected Vector2[] positionList;
        protected Dictionary<UserInputs, ICommand>[] cursorIndexToCommandList;
        protected int optionCount;
        private SpriteFont font;
        public readonly GameState identity;

        // Called once per frame to update sprites
        public abstract void Update(GameTime gameTime);

        // Draws sprites on the screen
        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void PopulateCursorIndexToCommandList();

        public abstract void PopulateCursorIndexToPositionList();

        public ICommand LoadCommand(UserInputs input)
        {
            return cursorIndexToCommandList[cursorIndex][input];
        }

        public void changeCursorIndex(int deltaIndex)
        {
            cursorIndex = (cursorIndex + deltaIndex) % (optionCount);
        }
    }
}
