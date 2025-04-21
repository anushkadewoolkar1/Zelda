using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using MainGame.CollisionHandling;
using ZeldaGame.Zelda.CollisionMap;

namespace MainGame.Sprites
{
    public class EnemyProjectileSprite : IGameObject, ISprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        private List<Rectangle> SourceRectangles = new();
        public Enemy enemy;
        public ItemType ItemType;
        private Vector2 enemyPosition;
        private Vector2 position;
        private Rectangle sourceRectangle;
        private Vector2 velocity;
        private Vector2 originalPosition;
        private Direction Direction;
        private int XSize = 0;
        private int YSize = 0;
        private int currentFrame;
        private int totalFrames;
        public int spriteSize;
        private double totalElapsed = 0.0;
        private bool spawned = false;
        private TileMap TileMap = TileMap.GetInstance();

        private const int DELAY_LENGTH = 150;
        public EnemyProjectileSprite(Texture2D texture, int rows, int columns, int startX, int startY, int xSize, int ySize, ItemType itemType)
        {
            Texture = texture;
            Rows = rows;
            Cols = columns;
            XSize = xSize;
            XSize = ySize;
            currentFrame = 0;
            totalFrames = Rows * Cols;
            for (int i = 0; i < totalFrames; i++)
            {
                SourceRectangles.Add(new Rectangle(startX + (i * xSize), startY, xSize, ySize));
            }

            velocity = new Vector2(0, 0);

            ItemType = itemType;
        }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            totalElapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (totalElapsed >= DELAY_LENGTH)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }

                totalElapsed -= DELAY_LENGTH;
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _position)
        {
            Rectangle destinationRectangle;
            
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, spriteSize, spriteSize * 2);


            spriteBatch.Draw(Texture, destinationRectangle, SourceRectangles[currentFrame], Color.White);

        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(position.X + originalPosition.X * 2), (int)(position.Y + originalPosition.Y * 2),
                    XSize, YSize);
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
        }

        public void Destroy()
        {
            sourceRectangle = new Rectangle((int)originalPosition.X, (int)originalPosition.Y, 0, 0);
        }

        public void Update(GameTime gameTime, Link link)
        {
            //no-op
        }
    }
}
