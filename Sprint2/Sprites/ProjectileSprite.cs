﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.CollisionHandling;
using Zelda.Enums;
using MainGame.States;
using System;

namespace MainGame.Sprites
{
    public class ProjectileSprite : IGameObject, ISprite
    {

        private Texture2D _texture;

        public Rectangle sourceRectangle { get; set; }
        private Vector2 destinationOrigin;
        public double[] deltaPosition { get; set; }

        public float rotation { get; set; }
        public int directionProjectile { get; set; }
        public Boolean isEnemyProjectile { get; set; } = false;
        private bool isSwordBeam;
        private Vector2 velocity;
        private Vector2 position;
        private ItemType currentProjectile;

        private bool destroy;

        private const int PROJECTILE_SCALE = 2;
        private const int X_INDEX = 0;
        private const int Y_INDEX = 1;
        private const int WIDTH_INDEX = 2;
        private const int HEIGHT_INDEX = 3;
        private const int PROJECTILE_SIZE = 16;
        private const int OOB_COORD = 1000;
        private const int SKINNY_PROJECTILE_YCOORD = 185;
        private const int ARROW_XCOORD = 30;
        private const int BOMB_XCOORD = 129;
        private const int SWORDBEAM_YCOORD = 154;

        private ProjectileClient projectileClient;
        private ProjectileDecorator projectileDecorator;

        public ProjectileSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int direction)
        {
            _texture = texture;

            destroy = false;
            isSwordBeam = false;

            // Rotates projectile depending on direction using mathhelper
            rotation = -direction * (MathHelper.Pi / 2);

            // Used for maintaining direction in Update method
            directionProjectile = direction;

            int[] sourceRectangleDimensions = AdjustProjectile(spriteSheetXPos, spriteSheetYPos, direction);

            // Hold the change in position after constructor call for ProjectileSprite
            deltaPosition = [0, 0];

            sourceRectangle = new Rectangle(sourceRectangleDimensions[X_INDEX], sourceRectangleDimensions[Y_INDEX],
                sourceRectangleDimensions[WIDTH_INDEX], sourceRectangleDimensions[HEIGHT_INDEX]);

            velocity = new Vector2(0, 0);

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


            //Keeps track of link's previous position
            position = _position;

            projectileClient.Draw(spriteBatch, position);

            return;
        }

        public void Update(GameTime gameTime, Link link)
        {
            HandleSwordBeam(link, destroy);

            //Returns when destroyed but let's link know when swordbeam is inactive
            if (destroy)
            {
                return;
            }

            if (isSwordBeam) link.swordBeam = true;

            projectileClient.Update(gameTime, link);


            return;
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
            int[] sourceRectangleDimensions = { xCoordinate, yCoordinate, PROJECTILE_SIZE, PROJECTILE_SIZE };


            //Checks if projectile is smaller than a 16x16 rectangle and adjusts accordingly
            if (yCoordinate == SKINNY_PROJECTILE_YCOORD && (xCoordinate < ARROW_XCOORD || xCoordinate == BOMB_XCOORD))
            {
                sourceRectangleDimensions[2] = 8;
                if (xCoordinate == BOMB_XCOORD)
                {
                    // Projectile Decorator for bomb
                    projectileDecorator = new ProjectileDecoratorExplode(new ProjectileConcrete(), this);
                    currentProjectile = ItemType.Bomb;
                }
                else
                {
                    // ProjectileDecorator for arrow
                    projectileDecorator = new ProjectileDecoratorLinear(new ProjectileConcrete(), this);
                    currentProjectile = ItemType.Arrow;
                }
            }
            else if (xCoordinate > ARROW_XCOORD && xCoordinate < BOMB_XCOORD)
            {
                // ProjectileDecorator for boomerang
                projectileDecorator = new ProjectileDecoratorRotate(new ProjectileDecoratorTrack(new ProjectileDecoratorReturn(
                    new ProjectileDecoratorLinear(new ProjectileConcrete(), this), this), this), this);
                sourceRectangleDimensions = [xCoordinate, yCoordinate, 5, 8];
                currentProjectile = ItemType.Boomerang;
            }
            else if (yCoordinate == SWORDBEAM_YCOORD)
            {
                //ProjectileDecorator for sword beam
                projectileDecorator = new ProjectileDecoratorLinear(new ProjectileConcrete(), this);
                sourceRectangleDimensions = [xCoordinate, yCoordinate, 5, 16];
                currentProjectile = ItemType.Arrow;
                ToggleSwordBeam();
            }
            else
            {
                //Handles currentProjectile Exception cases
                currentProjectile = ItemType.Arrow;
            }

            // Sets up client for Draw and Update
            projectileClient = new ProjectileClient(new ProjectileDecoratorTimeOut(projectileDecorator, this));

            return sourceRectangleDimensions;
        }

        public ItemType ReturnCurrentProjectile()
        {
            return currentProjectile;
        }

        public void Destroy()
        {
            sourceRectangle = new Rectangle(OOB_COORD, OOB_COORD, 0, 0);
            destroy = true;
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(position.X + deltaPosition[X_INDEX] * PROJECTILE_SCALE), (int)(position.Y + deltaPosition[Y_INDEX] * PROJECTILE_SCALE),
                    sourceRectangle.Width, sourceRectangle.Height);
            }
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            // no-op
        }


        private void ToggleSwordBeam()
        {
            isSwordBeam = !isSwordBeam;
        }

        private void HandleSwordBeam(Link link, Boolean destroy)
        {
            if (!link.swordBeam && destroy) return;

            if (destroy)
            {
                isSwordBeam = false;
            }

            link.swordBeam = isSwordBeam;
        }
    }

}