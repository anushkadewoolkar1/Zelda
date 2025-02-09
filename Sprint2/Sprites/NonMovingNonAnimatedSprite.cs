﻿using System;
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
}
