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
        private bool isBoomerang;
        private bool isBomb;
        private bool isSwordBeam;
        private Vector2 velocity;
        private Vector2 linkPosition;
        private Vector2 position;
        private ItemType currentProjectile;

        private int boomerangChangeDirection;
        private int timer;

        public bool destroy { get; set; }

        private const int PROJECTILE_SCALE = 2;
        private const int BASE_OFFSET_Y = 0;
        private const int BASE_INFLATE_Y = 0;
        private const int LGBOMB_OFFSET_X = 17;
        private const int SMBOMB_OFFSET_X = 13;
        private const int SMBOMB_INFLATE_X = 4;
        private const int DOWN = 0;
        private const int LEFT = 1;
        private const int RIGHT = 2;
        private const int UP = 3;
        private const int X_INDEX = 0;
        private const int Y_INDEX = 1;
        private const int WIDTH_INDEX = 2;
        private const int HEIGHT_INDEX = 3;
        private const int ABS_BMRNG_VEL = 2;
        private const int BOMB_BLOW_ALMOST = 20;
        private const int BOMB_BLOW_START = 25;
        private const int BOMB_BLOW_MIDDLE = 30;
        private const int BOMB_BLOW_FINISH = 35;
        private const int BMRNG_REVERSE_TIME = 30;
        private const int BMRNG_LIFESPAN = 60;
        private const int ITEM_LIFESPAN = 70;
        private const int PROJECTILE_SIZE = 16;
        private const int OOB_COORD = 200;
        private const double ABS_SLOW_BMRNG_VEL = 1.75;


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


            sourceRectangle = new Rectangle(sourceRectangleDimensions[X_INDEX], sourceRectangleDimensions[Y_INDEX],
                sourceRectangleDimensions[WIDTH_INDEX], sourceRectangleDimensions[HEIGHT_INDEX]);

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
            spriteBatch.Draw(_texture, new Vector2((int)(_position.X + deltaPosition[X_INDEX] * PROJECTILE_SCALE), (int)(_position.Y + deltaPosition[Y_INDEX] * PROJECTILE_SCALE)),
                sourceRectangle, Color.White, rotation, destinationOrigin, PROJECTILE_SCALE, SpriteEffects.None, 0f);

            _position = position;

            if (isBomb)
            {
                if (timer == BOMB_BLOW_ALMOST)
                {
                    sourceRectangle.Offset(SMBOMB_OFFSET_X, BASE_OFFSET_Y);
                    sourceRectangle.Inflate(SMBOMB_INFLATE_X, BASE_INFLATE_Y);
                }
                if (timer == BOMB_BLOW_START)
                {
                    sourceRectangle.Offset(LGBOMB_OFFSET_X, BASE_OFFSET_Y);
                }
                if (timer == BOMB_BLOW_MIDDLE)
                {
                    sourceRectangle.Offset(LGBOMB_OFFSET_X, BASE_OFFSET_Y);
                }
                if (timer == BOMB_BLOW_FINISH)
                {
                    this.Destroy();
                }
            }
        }

        public void Update(GameTime gameTime, Link link)
        {

            if (destroy)
            {
                return;
            }

            if (directionProjectile == DOWN)
            {
                if (boomerangChangeDirection >= BMRNG_REVERSE_TIME)
                {
                    deltaPosition[Y_INDEX] += ABS_BMRNG_VEL;
                    velocity = new Vector2(0, -1 * ABS_BMRNG_VEL * PROJECTILE_SCALE);

                } else
                {
                    deltaPosition[Y_INDEX] -= ABS_BMRNG_VEL;
                    velocity = new Vector2(0, ABS_BMRNG_VEL * PROJECTILE_SCALE);
                }
            } else if (directionProjectile == LEFT)
            {
                if (boomerangChangeDirection >= BMRNG_REVERSE_TIME)
                {
                    deltaPosition[X_INDEX] -= ABS_BMRNG_VEL;
                    velocity = new Vector2(-1 * ABS_BMRNG_VEL * PROJECTILE_SCALE, 0);
                }
                else
                {
                    deltaPosition[X_INDEX] += ABS_BMRNG_VEL;
                    velocity = new Vector2(ABS_BMRNG_VEL * PROJECTILE_SCALE, 0);
                }
            } else if (directionProjectile == RIGHT)
            {
                if (boomerangChangeDirection >= BMRNG_REVERSE_TIME)
                {
                    deltaPosition[Y_INDEX] -= ABS_BMRNG_VEL;
                    velocity = new Vector2(0, -1 * ABS_BMRNG_VEL * PROJECTILE_SCALE);
                }
                else
                {
                    deltaPosition[Y_INDEX] += ABS_BMRNG_VEL;
                    velocity = new Vector2(0, ABS_BMRNG_VEL * PROJECTILE_SCALE);
                }
            } else if (directionProjectile == UP)
            {
                if (boomerangChangeDirection >= BMRNG_REVERSE_TIME)
                {
                    deltaPosition[0] += ABS_BMRNG_VEL;
                    velocity = new Vector2(ABS_BMRNG_VEL * PROJECTILE_SCALE, 0);
                }
                else
                {
                    deltaPosition[0] -= ABS_BMRNG_VEL;
                    velocity = new Vector2(-1 * ABS_BMRNG_VEL * PROJECTILE_SCALE, 0);
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
                        deltaPosition[Y_INDEX] -= ABS_SLOW_BMRNG_VEL;
                    }
                    else if ((int)(linkPosition.Y - link.Position.Y) < 0)
                    {
                        deltaPosition[Y_INDEX] += ABS_SLOW_BMRNG_VEL;
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
                        deltaPosition[X_INDEX] -= ABS_SLOW_BMRNG_VEL;
                    }
                    else if ((int)(linkPosition.X - link.Position.X) < 0)
                    {
                        deltaPosition[X_INDEX] += ABS_SLOW_BMRNG_VEL;
                    }
                }
            }

            linkPosition = link.Position;

            timer++;

            // Rotates specifically if ProjectileSprite is boomerang and has the boomerang change it's direction
            if (isBoomerang)
            {
                rotation += (MathHelper.Pi / 8);
                boomerangChangeDirection++;
            }


            if ((timer >= BMRNG_LIFESPAN && isBoomerang) || timer >= ITEM_LIFESPAN)
            {
                sourceRectangle = new Rectangle(OOB_COORD, OOB_COORD, 0, 0);
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
            int[] sourceRectangleDimensions = {xCoordinate, yCoordinate, PROJECTILE_SIZE, PROJECTILE_SIZE};


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
                sourceRectangleDimensions[WIDTH_INDEX] = 5;
                sourceRectangleDimensions[HEIGHT_INDEX] = 8;
                isBoomerang = true;
                return sourceRectangleDimensions;
            }

            if (yCoordinate == 154)
            {
                isSwordBeam = true;
                sourceRectangleDimensions[WIDTH_INDEX] = 5;
                sourceRectangleDimensions[HEIGHT_INDEX] = 16;
            }

            return sourceRectangleDimensions;
        }

        public void Destroy()
        {
            sourceRectangle = new Rectangle((int)deltaPosition[X_INDEX], (int)deltaPosition[Y_INDEX], 0, 0);
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
                return new Rectangle((int)(position.X + deltaPosition[X_INDEX] * PROJECTILE_SCALE), (int)(position.Y + deltaPosition[Y_INDEX] * PROJECTILE_SCALE),
                    sourceRectangle.X, sourceRectangle.Y);
            }
        }
    }
}
