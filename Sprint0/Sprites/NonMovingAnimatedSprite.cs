using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{
    public class NonMovingAnimatedSprite : ISprite
    {
        private Texture2D _texture;
        private Vector2 _position;

        private List<Rectangle> _frames;
        private int _currentFrameIndex;
        private double _timeSinceLastFrame;
        private double _frameInterval; // in seconds

        public NonMovingAnimatedSprite(Texture2D texture, Vector2 position, List<Rectangle> frames, double frameInterval)
        {
            _texture = texture;
            _position = position;
            _frames = frames;
            _frameInterval = frameInterval;

            _currentFrameIndex = 0;
            _timeSinceLastFrame = 0.0;
        }

        public void Update(GameTime gameTime)
        {
            // Accumulate elapsed time
            _timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

            // If enough time has passed, move to the next frame
            if (_timeSinceLastFrame >= _frameInterval)
            {
                _currentFrameIndex++;
                if (_currentFrameIndex >= _frames.Count)
                {
                    _currentFrameIndex = 0;  // loop back to first frame
                }

                _timeSinceLastFrame = 0.0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = _frames[_currentFrameIndex];

            spriteBatch.Draw(_texture, _position, sourceRectangle, Color.White);

        }
    }
}