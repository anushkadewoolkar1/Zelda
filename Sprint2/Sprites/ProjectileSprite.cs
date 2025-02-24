using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private int projectileScale = 2;
        private bool isBoomerang;
        private bool isBomb;

        private int boomerangChangeDirection;
        private int timer;

        public ProjectileSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int direction)
        {
            _texture = texture;
            isBomb = false;
            isBoomerang = false;
            
            // Rotates projectile depending on direction using mathhelper
            rotation = direction * (MathHelper.Pi / 2);

            // Used for maintaining direction in Update method
            directionProjectile = direction;

            int[] sourceRectangleDimensions = AdjustProjectile(spriteSheetXPos, spriteSheetYPos, direction);

            // Hold the change in position after constructor call for ProjectileSprite
            deltaPosition = [0, 0];


            sourceRectangle = new Rectangle(sourceRectangleDimensions[0], sourceRectangleDimensions[1],
                sourceRectangleDimensions[2], sourceRectangleDimensions[3]);

            boomerangChangeDirection = 0;
        }

        //When calling this Draw, Position is the center of sprite
        public void Draw(SpriteBatch spriteBatch, Vector2 _position)
        {
            destinationOrigin = new Vector2(((int)sourceRectangle.Width) / 2, ((int)sourceRectangle.Height) / 2);
            spriteBatch.Draw(_texture, new Vector2((int)_position.X + deltaPosition[0] * projectileScale, (int)_position.Y + deltaPosition[1] * projectileScale),
                sourceRectangle, Color.White, rotation, destinationOrigin, projectileScale, SpriteEffects.None, 0f);

            if (isBomb)
            {
                if (timer == 20)
                {
                    sourceRectangle.Offset(13, 0);
                    sourceRectangle.Inflate(4, 0);
                }
                if (timer == 25)
                {
                    sourceRectangle.Offset(17, 0);
                }
                if (timer == 30)
                {
                    sourceRectangle.Offset(17, 0);
                }
                if (timer == 35)
                {
                    sourceRectangle.Offset(36, 30);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (directionProjectile == 0)
            {
                if (boomerangChangeDirection >= 40)
                {
                    deltaPosition[1] += 2;
                } else
                {
                    deltaPosition[1] -= 2;
                }
            } else if (directionProjectile == 1)
            {
                if (boomerangChangeDirection >= 40)
                {
                    deltaPosition[0] -= 2;
                }
                else
                {
                    deltaPosition[0] += 2;
                }
            } else if (directionProjectile == 2)
            {
                if (boomerangChangeDirection >= 40)
                {
                    deltaPosition[1] -= 2;
                }
                else
                {
                    deltaPosition[1] += 2;
                }
            } else if (directionProjectile == 3)
            {
                if (boomerangChangeDirection >= 40)
                {
                    deltaPosition[0] += 2;
                }
                else
                {
                    deltaPosition[0] -= 2;
                }
            }

            timer++;

            // Rotates specifically if ProjectileSprite is boomerang and has the boomerang change it's direction
            if (isBoomerang)
            {
                rotation += (MathHelper.Pi / 8);
                boomerangChangeDirection += 1;
            }

        }

        public void Draw(SpriteBatch _textures)
        {
            //no-op
        }

        private int[] AdjustProjectile(int xCoordinate, int yCoordinate, int direction)
        {
            int[] sourceRectangleDimensions = {xCoordinate, yCoordinate, 16, 16};


            //Checks if projectile is smaller than a 16x16 rectangle and adjusts accordingly
            if (yCoordinate == 185 && (xCoordinate < 29 || xCoordinate == 129))
            {
                sourceRectangleDimensions[2] = 8;
                if (xCoordinate == 129)
                {
                    isBomb = true;
                }
                return sourceRectangleDimensions;
            } else if (xCoordinate > 30 && xCoordinate < 129)
            {
                sourceRectangleDimensions[2] = 5;
                sourceRectangleDimensions[3] = 8;
                isBoomerang = true;
                return sourceRectangleDimensions;
            }

            return sourceRectangleDimensions;
        }
    }
}
