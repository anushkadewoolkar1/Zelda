using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{
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

		public MovingAnimatedSprite(Texture2D texture, Vector2 startPosition, Vector2 velocity, List<Rectangle> frames, double frameInterval)
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
}