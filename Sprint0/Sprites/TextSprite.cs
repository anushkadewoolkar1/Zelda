using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{

    public class TextSprite : ISprite
    {
        private SpriteFont _font;
        private string _text;
        private Vector2 _position;
        private Color _color;

        public TextSprite(SpriteFont font, string text, Vector2 position, Color color)
        {
            _font = font;
            _text = text;
            _position = position;
            _color = color;
        }

        public void Update(GameTime gameTime)
        {
            // not needed
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(_font, _text, _position, _color);

        }
    }

}