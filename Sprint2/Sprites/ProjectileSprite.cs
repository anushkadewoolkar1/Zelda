using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;

namespace Sprint0.Sprites
{
    public class ProjectileSprite : ISprite
    {

        private Texture2D _texture;

        private Rectangle sourceRectangle;
        private Vector2 destinationOrigin;
        private int[] deltaPosition;

        private float rotation;
        private int directionProjectile;

        public ProjectileSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int direction)
        {
            _texture = texture;
            
            rotation = direction * (MathHelper.Pi / 2);
            directionProjectile = direction;

            int[] sourceRectangleDimensions = AdjustProjectile(spriteSheetXPos, spriteSheetYPos, direction);

            deltaPosition = [0, 0];


            sourceRectangle = new Rectangle(sourceRectangleDimensions[0], sourceRectangleDimensions[1], sourceRectangleDimensions[2]
                       , sourceRectangleDimensions[3]);
        }

        //When calling this Draw, Position is the center of sprite
        public void Draw(SpriteBatch spriteBatch, Vector2 _position)
        {
            destinationOrigin = new Vector2(((int)sourceRectangle.Width) / 2, ((int)sourceRectangle.Height) / 2);
            spriteBatch.Draw(_texture, new Vector2((int)_position.X + deltaPosition[0], (int)_position.Y + deltaPosition[1]), sourceRectangle, Color.White, rotation,
                destinationOrigin, 1.0f, SpriteEffects.None, 0f);
            if ((int) sourceRectangle.Width == 5)
            {
                rotation += (MathHelper.Pi / 8);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (directionProjectile == 0)
            {
                deltaPosition[1] -= 2;
            } else if (directionProjectile == 1)
            {
                deltaPosition[0] += 2;
            } else if (directionProjectile == 2)
            {
                deltaPosition[1] += 2;
            } else if (directionProjectile == 3)
            {
                deltaPosition[0] -= 2;
            }
        }

        public void Draw(SpriteBatch _textures)
        {
            //no-op
        }

        private int[] AdjustProjectile(int xCoordinate, int yCoordinate, int direction)
        {
            int[] sourceRectangleDimensions = {xCoordinate, yCoordinate, 16, 16};



            if (yCoordinate == 185 && (xCoordinate < 29 || xCoordinate == 129))
            {
                sourceRectangleDimensions[2] = 7;
                return sourceRectangleDimensions;
            } else if (xCoordinate > 30 && xCoordinate < 129)
            {
                sourceRectangleDimensions[2] = 5;
                sourceRectangleDimensions[3] = 8;
                return sourceRectangleDimensions;
            }

            return sourceRectangleDimensions;
        }
    }
}
