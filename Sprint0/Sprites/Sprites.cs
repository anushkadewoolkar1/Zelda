using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{
    public class NonMovingNonAnimatedSprite : ISprite
    {
        private Texture2D _texture;
        private Vector2 _position;

        public NonMovingNonAnimatedSprite(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }

        public void Update(GameTime gameTime)
        {
            // No movement or animation
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            
            spriteBatch.Draw(_texture, _position, Color.White);
            
        }
    }

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

    public class MovingAnimatedSprite : ISprite 
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;

        // animation fields
        private List<Rectangle> _frames;
        private int _currentFrameIndex;

        private double _timeSinceLastFrame;
        private double _frameInterval;

        private int _screenWidth;
        private int _screenHeight;

        public MovingAnimatedSprite(Texture2D texture,Vector2 startPosition, Vector2 velocity, List<Rectangle> frames, double frameInterval)
        {
            _texture = texture;
            _position = startPosition;
            _velocity = velocity;
            _frames = frames;
            _frameInterval = frameInterval;

            _currentFrameIndex = 0;
            _timeSinceLastFrame = 0.0; ;        
        }

        public void Update(GameTime gameTime) 
        {
            // Move
            _position += _velocity;
           
            int frameWidth = _frames[_currentFrameIndex].Width;
            int frameHeight = _frames[_currentFrameIndex].Height;

            // Animate
            _timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeSinceLastFrame >= _frameInterval)
            {
                _currentFrameIndex++;
                if (_currentFrameIndex >= _frames.Count)
                {
                    _currentFrameIndex = 0; // loop back
                }
                _timeSinceLastFrame = 0.0;
            }
            
            // Bounce horizontally
            if (_position.X > 790)
            {
                // Reverse horizontal velocity
                _velocity.X = -_velocity.X;
            }
            else if (_position.X < 1)
            {
                // Reverse horizontal velocity
                _velocity.X = -_velocity.X;
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            Rectangle sourceRect = _frames[_currentFrameIndex];

            
            spriteBatch.Draw(_texture, _position, sourceRect, Color.White);
            
        }
    }

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
