using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.States;

namespace MainGame.Sprites
{
    public class LinkSprite : ISprite
    {
        public const int RECTANGLE_DIM = 16;
        private Texture2D _texture;
        public enum LinkSpriteDirection { Down, Left, Right, Up };

        private Rectangle sourceRectangle;
        private int colorAdjustment;
        private bool linkDamaged;
        private int damageClock;
        private bool death;

        private int leftAdjustment;
        private int upAdjustment;
        private const int linkScale = 2;

        private const int UP_DIRECTION = 3;
        private const int COLOR_SCALE = 310;
        private const int RIGHT_BOUNDARY_COORD = 742;
        private readonly Link _link;
        public float Scale { get; set; } = 2f;

        public LinkSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int[] linkStates, Link link)
        {
            _texture = texture;
            death = false;
            _link = link;
            linkDamaged = !(Convert.ToBoolean(linkStates[3]));
            if (!linkDamaged)
            {
                damageClock = 0;
            }
            leftAdjustment = 0;
            upAdjustment = 0;

            // Use moved helper for attack adjustments
            sourceRectangle = new Rectangle();
            var dims = LinkAttackHelper.AdjustAttacks(spriteSheetXPos, spriteSheetYPos, linkStates[1], linkStates[0], out leftAdjustment, out upAdjustment);

            if (linkStates[0] < UP_DIRECTION && (linkStates[2] % 2) == 1)
            {
                dims = LinkAttackHelper.MagicalShield(dims, linkStates[1]);
            }

            colorAdjustment = (linkStates[2] / 2) * COLOR_SCALE;

            if (linkStates[2] == -1)
            {
                sourceRectangle = new Rectangle(FixDirection(dims[0], linkStates[0], dims[2]), dims[1], dims[2], dims[3]);
                death = true;
                return;
            }

            switch (linkStates[1])
            {
                case 1:
                    sourceRectangle = new Rectangle(FixDirection(dims[0], linkStates[0], dims[2]), dims[1] + colorAdjustment, dims[2], dims[3]);
                    break;
                case 2:
                    sourceRectangle = new Rectangle(FixDirection(dims[0] + 17, linkStates[0], dims[2]), dims[1] + colorAdjustment, dims[2], dims[3]);
                    break;
                case 3:
                    sourceRectangle = new Rectangle(FixDirection(dims[0] + 34, linkStates[0], dims[2]), dims[1] + colorAdjustment, dims[2], dims[3]);
                    break;
                case 4:
                    sourceRectangle = new Rectangle(FixDirection(dims[0] + 51, linkStates[0], dims[2]), dims[1] + colorAdjustment, dims[2], dims[3]);
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _position)
        {
            Vector2 origin = new Vector2(8, 8);

            //This is used when Link is attacking Left or Up (Adjusts rectange accordingly)
            _position.X -= leftAdjustment * linkScale;
            _position.Y -= upAdjustment * linkScale;


            if (death)
            {
                spriteBatch.Draw(
                    _texture,
                    _position,
                    sourceRectangle,
                    Color.Gray,
                    0f,
                    origin,
                    _link.LinkScale,
                    SpriteEffects.None,
                    0f);
                return;
            }

            if (!linkDamaged)
            {
                spriteBatch.Draw(
                    _texture,
                    _position,
                    sourceRectangle,
                    Color.White,
                    0f,
                    origin,
                    _link.LinkScale,
                    SpriteEffects.None,
                    0f);
            }
            else
            {
                Color flickerColor = Color.White;
                switch (damageClock % 3)
                {
                    case 0: flickerColor = Color.Purple; break;
                    case 1: flickerColor = Color.Red; break;
                    case 2: flickerColor = Color.Blue; break;
                }

                spriteBatch.Draw(
                    _texture,
                    _position,
                    sourceRectangle,
                    flickerColor,
                    0f,
                    origin,
                    _link.LinkScale,
                    SpriteEffects.None,
                    0f);

                damageClock++;
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


        //Adjusts the xCoordinate to rightside of spritesheet if link is facing left
        private int FixDirection(int xCoordinates, int direction, int sourceRectangleWidth)
        {
            if (direction != (int)LinkSpriteDirection.Left)
            {
                return xCoordinates;
            }
            else
            {
                return RIGHT_BOUNDARY_COORD - xCoordinates - sourceRectangleWidth;
            }
        }

        public void Update(GameTime gameTime, Link link)
        {
            //no-op
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            // no-op
        }
    }
}
