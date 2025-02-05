using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{
    public class MovingNonAnimatedSprite : ISprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;

        public MovingNonAnimatedSprite(Texture2D texture, Vector2 startPosition, Vector2 velocity)
        {
            _texture = texture;
            _position = startPosition;
            _velocity = velocity;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // move sprite
            _position += deltaTime * _velocity;

            // Bounce vertically
            if (_position.Y > 360)
            {
                // Reverse  velocity
                _velocity.Y = -_velocity.Y;
            }
            else if (_position.Y < 1)
            {
                // Reverse velocity
                _velocity.Y = -_velocity.Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, _position, Color.White);

        }
    }
}