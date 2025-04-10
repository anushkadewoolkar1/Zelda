using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainGame.Sprites
{
    public abstract class ProjectileComponent
    {
        public abstract void Update(GameTime gameTime, Link link);
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 position);
    }

    // Used for implementation of Completely Static Objects
    public class ProjectileConcrete : ProjectileComponent
    {
        public override void Update(GameTime gameTime, Link link)
        {
            // no-op
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // no-op
        }
    }

    // Decorator Design Pattern
    public class ProjectileDecorator : ProjectileComponent
    {
        private ProjectileComponent _projectileComponent;
        public ProjectileDecorator(ProjectileComponent projectileComponent)
        {
            _projectileComponent = projectileComponent;
        }
        public override void Update(GameTime gameTime, Link link)
        {
            _projectileComponent.Update(gameTime, link);
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            _projectileComponent.Draw(spriteBatch, position);
        }
    }

    //Handles explosions or shifting sprites
    public class ProjectileDecoratorExplode : ProjectileDecorator
    {
        private const int BOMB_BLOW_BEGIN = 20;
        private const int BOMB_BLOW_STAGES = 5;
        private const int BOMB_BLOW_FINISH = 35;

        private const int LGBOMB_OFFSET_X = 17;
        private const int SMBOMB_OFFSET_X = 13;
        private const int SMBOMB_INFLATE_X = 4;

        private const int BASE_OFFSET_Y = 0;
        private const int BASE_INFLATE_Y = 0;

        private int timer;
        private Rectangle sourceRectangle;
        private ProjectileSprite projectileSprite;

        public ProjectileDecoratorExplode(ProjectileComponent projectileComponent, ProjectileSprite ProjectileSprite ) : base(projectileComponent)
        {
            timer = 0;
            projectileSprite = ProjectileSprite;
            sourceRectangle = ProjectileSprite.sourceRectangle;

        }
        public override void Update(GameTime gameTime, Link link)
        {
            timer++;
            base.Update(gameTime, link);
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            sourceRectangle = projectileSprite.sourceRectangle;

            // Handles the explosion of the bomb
            if (timer >= (BOMB_BLOW_BEGIN) && timer % BOMB_BLOW_STAGES == 0)
            {
                if (timer == BOMB_BLOW_BEGIN)
                {
                    sourceRectangle.Offset(SMBOMB_OFFSET_X, BASE_OFFSET_Y);
                    sourceRectangle.Inflate(SMBOMB_INFLATE_X, BASE_INFLATE_Y);
                }
                else if (timer == BOMB_BLOW_FINISH)
                {
                    projectileSprite.Destroy();
                }
                else
                {
                    sourceRectangle.Offset(LGBOMB_OFFSET_X, BASE_OFFSET_Y);
                }

            }
            projectileSprite.sourceRectangle = sourceRectangle;
            base.Draw(spriteBatch, position);
        }
    }

    //Handles linear movement of projectile
    public class ProjectileDecoratorLinear : ProjectileDecorator
    {
        private ProjectileSprite projectileSprite;
        private int directionProjectile;

        private const int X_INDEX = 0;
        private const int Y_INDEX = 1;

        private const int ABS_VEL = 2;
        private const int PROJECTILE_SCALE = 2;


        public ProjectileDecoratorLinear(ProjectileComponent projectileComponent, ProjectileSprite ProjectileSprite) : base(projectileComponent)
        {
            projectileSprite = ProjectileSprite;
            directionProjectile = projectileSprite.directionProjectile;

        }
        public override void Update(GameTime gameTime, Link link)
        {
            //Handles projectile movement
            if (directionProjectile % 2 == 0 && directionProjectile >= 0)
            {
                projectileSprite.deltaPosition[Y_INDEX] += (ABS_VEL * (directionProjectile - 1));
                projectileSprite.Velocity = new Vector2(0, (directionProjectile - 1) * ABS_VEL * PROJECTILE_SCALE);

            }
            else if (directionProjectile >= 0)
            {
                projectileSprite.deltaPosition[X_INDEX] += (ABS_VEL * (directionProjectile - 2));
                projectileSprite.Velocity = new Vector2((directionProjectile - 2) * ABS_VEL * PROJECTILE_SCALE, 0);
            }
            base.Update(gameTime, link);
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // No Special Draw for Linear
            base.Draw(spriteBatch, position);
        }
    }

    //Handles the projectile coming back
    public class ProjectileDecoratorReturn : ProjectileDecorator
    {
        private ProjectileSprite projectileSprite;
        private int timer;
        private int directionProjectile;

        private const int X_INDEX = 0;
        private const int Y_INDEX = 1;

        private const int ABS_VEL = 2;
        private const int PROJECTILE_SCALE = 2;

        private const int REVERSE_TIME = 30;


        public ProjectileDecoratorReturn(ProjectileComponent projectileComponent, ProjectileSprite ProjectileSprite) : base(projectileComponent)
        {
            projectileSprite = ProjectileSprite;
            directionProjectile = projectileSprite.directionProjectile;
            timer = 0;
        }

        public override void Update(GameTime gameTime, Link link)
        {
            //Handles comeback
            if (directionProjectile % 2 == 0 && directionProjectile >= 0 && timer >= REVERSE_TIME)
            {
                projectileSprite.deltaPosition[Y_INDEX] -= 2 * (ABS_VEL * (directionProjectile - 1));
                projectileSprite.Velocity = new Vector2(0, -(directionProjectile - 1) * ABS_VEL * PROJECTILE_SCALE);
                
            }
            else if (directionProjectile >= 0 && timer >= REVERSE_TIME)
            {
                projectileSprite.deltaPosition[X_INDEX] -= 2 * (ABS_VEL * (directionProjectile - 2));
                projectileSprite.Velocity = new Vector2(-(directionProjectile - 2) * ABS_VEL * PROJECTILE_SCALE, 0);
            }

            timer++;

            base.Update(gameTime, link);
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // no-op
            base.Draw(spriteBatch, position);
        }
    }

    //Handles the projectile tracking Link
    public class ProjectileDecoratorTrack : ProjectileDecorator
    {
        ProjectileSprite projectileSprite;
        private int directionProjectile;
        private Vector2 linkPosition;

        private const double ABS_SLOW_VEL = 1.75;

        private const int X_INDEX = 0;
        private const int Y_INDEX = 1;

        private const int DELETE_TIME = 30;

        private int timer = 0;

        private Vector2 position;

        public ProjectileDecoratorTrack(ProjectileComponent projectileComponent, ProjectileSprite ProjectileSprite) : base(projectileComponent)
        {
            projectileSprite = ProjectileSprite;
            directionProjectile = projectileSprite.directionProjectile;

            timer = 0;
        }
        public override void Update(GameTime gameTime, Link link)
        {
            if (directionProjectile % 2 == 1)
            {
                if ((int)(linkPosition.Y - link.Position.Y) > 0)
                {
                    projectileSprite.deltaPosition[Y_INDEX] -= ABS_SLOW_VEL;
                }
                else if ((int)(linkPosition.Y - link.Position.Y) < 0)
                {
                    projectileSprite.deltaPosition[Y_INDEX] += ABS_SLOW_VEL;
                }
                if ((((int)link.Position.X - (int)position.X - projectileSprite.deltaPosition[X_INDEX] * 2) <= 5 &&
                    ((int)link.Position.X - (int)position.X - projectileSprite.deltaPosition[X_INDEX] * 2) >= -5)
                    && timer >= DELETE_TIME)
                {
                    projectileSprite.Destroy();
                }
            }
            else
            {
                if ((int)(linkPosition.X - link.Position.X) > 0)
                {
                    projectileSprite.deltaPosition[X_INDEX] -= ABS_SLOW_VEL;
                }
                else if ((int)(linkPosition.X - link.Position.X) < 0)
                {
                    projectileSprite.deltaPosition[X_INDEX] += ABS_SLOW_VEL;
                }
                if ((((int)link.Position.Y - (int)position.Y - projectileSprite.deltaPosition[Y_INDEX] * 2) <= 5 &&
                    ((int)link.Position.Y - (int)position.Y - projectileSprite.deltaPosition[Y_INDEX] * 2) >= -5)
                    && timer >= DELETE_TIME)
                {
                    projectileSprite.Destroy();
                }
            }


            linkPosition = link.Position;
            timer++;

            base.Update(gameTime, link);
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            this.position = position;
            base.Draw(spriteBatch, position);
        }
    }

    //Handles the projectile rotating
    public class ProjectileDecoratorRotate : ProjectileDecorator
    {
        ProjectileSprite projectileSprite;

        public ProjectileDecoratorRotate(ProjectileComponent projectileComponent, ProjectileSprite ProjectileSprite) : base(projectileComponent)
        {
            projectileSprite = ProjectileSprite;
        }
        public override void Update(GameTime gameTime, Link link)
        {
            projectileSprite.rotation += (MathHelper.Pi / 8);
            base.Update(gameTime, link);
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            base.Draw(spriteBatch, position);
        }
    }

    //Handles the projectile timing out
    public class ProjectileDecoratorTimeOut : ProjectileDecorator
    {
        ProjectileSprite projectileSprite;
        private int timer;

        private const int LIFESPAN = 60;

        public ProjectileDecoratorTimeOut(ProjectileComponent projectileComponent, ProjectileSprite ProjectileSprite) : base(projectileComponent)
        {
            projectileSprite = ProjectileSprite;
        }
        public override void Update(GameTime gameTime, Link link)
        {
            if (timer >= LIFESPAN)
            {
                projectileSprite.Destroy();   
            }

            timer++;

            base.Update(gameTime, link);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            base.Draw(spriteBatch, position);
        }
    }

    // Is ProjectileClient of Decorator
    public class ProjectileClient
    {
        private ProjectileComponent projectileComponent;
        public ProjectileClient(ProjectileComponent ProjectileComponent)
        {
            projectileComponent = ProjectileComponent;
        }
        public void Update(GameTime gameTime, Link link)
        {
            projectileComponent.Update(gameTime, link);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            projectileComponent.Draw(spriteBatch, position);
        }

    }

}