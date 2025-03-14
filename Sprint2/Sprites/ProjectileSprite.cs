using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.CollisionHandling;
using Zelda.Enums;

namespace Sprint0.Sprites
{
    public class ProjectileSprite : IGameObject, ISprite
    {

        private Texture2D _texture;

        private Rectangle sourceRectangle;
        private Vector2 destinationOrigin;
        private double[] deltaPosition;

        private float rotation;
        private int directionProjectile;
        private int projectileScale = 2;
        private bool isBoomerang;
        private bool isBomb;
        private bool isSwordBeam;
        public Vector2 velocity;
        private Vector2 linkPosition;
        private Vector2 position;
        public ItemType currentProjectile;

        private int boomerangChangeDirection;
        private int timer;

        public bool destroy { get; set; }

        public ProjectileSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int direction)
        {
            _texture = texture;

            destroy = false;
            isBomb = false;
            isBoomerang = false;
            isSwordBeam = false;
            
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

            velocity = new Vector2(0, 0);

            if (isBomb)
            {
                currentProjectile = ItemType.Bomb;
            } else if (isBoomerang)
            {
                currentProjectile = ItemType.Boomerang;
            } else
            {
                currentProjectile = ItemType.Arrow;
            }
        }

        //When calling this Draw, Position is the center of sprite
        public void Draw(SpriteBatch spriteBatch, Vector2 _position)
        {

            if (destroy)
            {
                return;
            }

            destinationOrigin = new Vector2(((int)sourceRectangle.Width) / 2, ((int)sourceRectangle.Height) / 2);
            spriteBatch.Draw(_texture, new Vector2((int)(_position.X + deltaPosition[0] * projectileScale), (int)(_position.Y + deltaPosition[1] * projectileScale)),
                sourceRectangle, Color.White, rotation, destinationOrigin, projectileScale, SpriteEffects.None, 0f);

            _position = position;

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
                    sourceRectangle = new Rectangle((int)deltaPosition[0], (int)deltaPosition[1], 0, 0);
                    destroy = true;
                }
            }
        }

        public void Update(GameTime gameTime, Link link)
        {

            if (destroy)
            {
                return;
            }

            if (directionProjectile == 0)
            {
                if (boomerangChangeDirection >= 30)
                {
                    deltaPosition[1] += 2;
                    velocity = new Vector2(0, -2 * projectileScale);

                } else
                {
                    deltaPosition[1] -= 2;
                    velocity = new Vector2(0, 2 * projectileScale);
                }
            } else if (directionProjectile == 1)
            {
                if (boomerangChangeDirection >= 30)
                {
                    deltaPosition[0] -= 2;
                    velocity = new Vector2(-2 * projectileScale, 0);
                }
                else
                {
                    deltaPosition[0] += 2;
                    velocity = new Vector2(2 * projectileScale, 0);
                }
            } else if (directionProjectile == 2)
            {
                if (boomerangChangeDirection >= 30)
                {
                    deltaPosition[1] -= 2;
                    velocity = new Vector2(0, -2 * projectileScale);
                }
                else
                {
                    deltaPosition[1] += 2;
                    velocity = new Vector2(0, 2 * projectileScale);
                }
            } else if (directionProjectile == 3)
            {
                if (boomerangChangeDirection >= 30)
                {
                    deltaPosition[0] += 2;
                    velocity = new Vector2(2 * projectileScale, 0);
                }
                else
                {
                    deltaPosition[0] -= 2;
                    velocity = new Vector2(-2 * projectileScale, 0);
                }
            }

            if (directionProjectile % 2 == 1)
            {
                if (isBoomerang)
                {
                    //System.Diagnostics.Debug.WriteLine((((int)link.Position.Y)).ToString());
                    System.Diagnostics.Debug.WriteLine(
                        ((int)(linkPosition.Y - link.Position.Y)).ToString());
                    if ((int)(linkPosition.Y - link.Position.Y) > 0)
                    {
                        deltaPosition[1] -= 1.75;
                    }
                    else if ((int)(linkPosition.Y - link.Position.Y) < 0)
                    {
                        deltaPosition[1] += 1.75;
                    }
                }
            }
            else
            {
                if (isBoomerang)
                {
                    //System.Diagnostics.Debug.WriteLine((((int)link.Position.Y)).ToString());
                    System.Diagnostics.Debug.WriteLine(
                        ((int)(linkPosition.X - link.Position.X)).ToString());
                    if ((int)(linkPosition.X - link.Position.X) > 0)
                    {
                        deltaPosition[0] -= 1.75;
                    }
                    else if ((int)(linkPosition.X - link.Position.X) < 0)
                    {
                        deltaPosition[0] += 1.75;
                    }
                }
            }

            linkPosition = link.Position;

            timer++;

            // Rotates specifically if ProjectileSprite is boomerang and has the boomerang change it's direction
            if (isBoomerang)
            {
                rotation += (MathHelper.Pi / 8);
                boomerangChangeDirection += 1;
            }


            if ((timer >= 60 && isBoomerang) || timer >= 70)
            {
                sourceRectangle = new Rectangle(200, 200, 0, 0);
                destroy = true;
                if (isSwordBeam)
                {
                    link.swordBeam = false;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            //no-op
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

            if (yCoordinate == 154)
            {
                isSwordBeam = true;
                sourceRectangleDimensions[2] = 5;
                sourceRectangleDimensions[3] = 16;
            }

            return sourceRectangleDimensions;
        }

        public void Destroy()
        {
            sourceRectangle = new Rectangle((int)deltaPosition[0], (int)deltaPosition[1], 0, 0);
            destroy = true;
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
        }
        
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(position.X + deltaPosition[0] * projectileScale), (int)(position.Y + deltaPosition[1] * projectileScale),
                    sourceRectangle.X, sourceRectangle.Y);
            }
        }
    }
}
