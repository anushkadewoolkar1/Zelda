using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sprint0.CollisionHandling;
using Zelda.Enums;

namespace Sprint0.Display
{
    public class SettingsMenu
    {
        private Texture2D _pixel;
        private SpriteFont _font;

        public SettingsMenu(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
            _font = content.Load<SpriteFont>("DefaultFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var viewport = spriteBatch.GraphicsDevice.Viewport;
            int halfWidth = viewport.Width / 3 + 33;
            int halfHeight = viewport.Height / 3 - 8;

            // Draw black rectangle over 
            spriteBatch.Draw(
                _pixel,
                new Rectangle(0, 0, halfWidth, halfHeight),
                Color.Black
            );
            spriteBatch.DrawString(_font, "Resume Game", new Vector2(200, 150), Color.White);
            spriteBatch.DrawString(_font, "New Game", new Vector2(200, 175), Color.White);
            spriteBatch.DrawString(_font, "Quit Game", new Vector2(200, 200), Color.White);
        }

    }
}
