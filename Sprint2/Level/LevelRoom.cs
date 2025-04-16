
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Zelda.Enums;
using MainGame.Sprites;
using MainGame.CollisionHandling;
using MainGame.States;

namespace MainGame.Display
{
    public class Room : IGameObject
    {
        
        public List<Block> Blocks { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public List<Item> Items { get; private set; }

        // Bg/layout
        public int RoomWidth { get; private set; }
        public int RoomHeight { get; private set; }
        public Vector2 RoomDimensions { get; private set; }
        public Rectangle SourceRectangle { get; private set; }

        private Texture2D backgroundTexture;
        private ContentManager contentManager;

        private LevelManager level;

        public Room(ContentManager content, Texture2D backgroundTexture, LevelManager level)
        {
            this.contentManager = content;
            this.backgroundTexture = backgroundTexture;
            this.level = level;

            Blocks = new List<Block>();
            Enemies = new List<Enemy>();
            Items = new List<Item>();

            // Calculate room dimensions and the source rectangle.
            RoomWidth = (backgroundTexture.Width - LevelConstants.HEIGHT_POSITION) / LevelConstants.LEVEL_TEXTURE_SCALAR;
            RoomHeight = (backgroundTexture.Height - LevelConstants.HEIGHT_POSITION) / LevelConstants.LEVEL_TEXTURE_SCALAR;
            SourceRectangle = new Rectangle(
                (RoomWidth * LevelConstants.WIDTH_POSITION) + LevelConstants.SHIFT_INTO_RANGE,
                (RoomHeight * LevelConstants.HEIGHT_POSITION) + LevelConstants.SHIFT_INTO_RANGE,
                RoomWidth, RoomHeight);
            RoomDimensions = new Vector2((RoomWidth * LevelConstants.WIDTH_POSITION) - LevelConstants.LEVEL_CENTER_POSITION,
                                         (RoomHeight * LevelConstants.WIDTH_POSITION) - LevelConstants.LEVEL_CENTER_POSITION);
        }

        private Texture2D[] LoadBlockTextures()
        {
            return new Texture2D[]
            {
                contentManager.Load<Texture2D>("transparent_block") // Adjust filename as needed.
            };
        }

        /// <summary>
        /// Loads room object data from the level tokens. The starting token index should point just after the room
        /// coordinate tokens & the method will skip ROOM_STARTING_POINT tokens and create the room objs
        /// </summary>
        public void LoadRoomData(int xCoordinate, int yCoordinate, int startTokenIndex, List<string> tokens, List<IGameObject> globalObjects, Link link)
        {
            // Adjust the source rectangle based on room coordinates.
            SourceRectangle = new Rectangle(
                RoomWidth * xCoordinate + LevelConstants.SHIFT_INTO_RANGE * (xCoordinate + LevelConstants.SHIFT_INTO_RANGE),
                RoomHeight * yCoordinate + LevelConstants.SHIFT_INTO_RANGE * (yCoordinate + 1),
                RoomWidth, RoomHeight);

            int tokenIndex = startTokenIndex + LevelConstants.ROOM_STARTING_POINT;
            int roomTokenLimit = tokenIndex + LevelConstants.BLOCKS_PER_ROOM_X * LevelConstants.BLOCKS_PER_ROOM_Y;

            while (tokenIndex < roomTokenLimit && tokenIndex < tokens.Count)
            {
                string token = tokens[tokenIndex];
                int gridIndex = tokenIndex - (startTokenIndex + LevelConstants.ROOM_STARTING_POINT);
                if (token.Contains("Enemy"))
                {
                    CreateEnemy(tokenIndex, gridIndex, tokens, globalObjects);
                }
                else if (token.Contains("Block"))
                {
                    CreateBlock(tokenIndex, gridIndex, tokens, globalObjects);
                }
                else if (token.Contains("Item"))
                {
                    CreateItem(tokenIndex, gridIndex, tokens, globalObjects);
                }
                tokenIndex++;
            }
        }

        private void CreateEnemy(int tokenIndex, int gridIndex, List<string> tokens, List<IGameObject> globalObjects)
        {
            if (!Enum.TryParse(tokens[tokenIndex].Substring(LevelConstants.ENEMY_START_INDEX), out EnemyType enemyType))
                return;
            Enemy enemy = new Enemy(globalObjects, level.ReturnLink()._audio);
            enemy.CreateEnemy(enemyType,
                new Vector2((gridIndex % LevelConstants.BLOCKS_PER_ROOM_X) - LevelConstants.SHIFT_INTO_RANGE,
                            (gridIndex / LevelConstants.BLOCKS_PER_ROOM_X) - LevelConstants.SHIFT_INTO_RANGE));
            Enemies.Add(enemy);
            globalObjects.Add(enemy);
        }

        private void CreateBlock(int tokenIndex, int gridIndex, List<string> tokens, List<IGameObject> globalObjects)
        {
            Vector2 position = new Vector2(
                (RoomDimensions.X / LevelConstants.BLOCKS_PER_ROOM_X) * (gridIndex % LevelConstants.BLOCKS_PER_ROOM_X) + LevelConstants.BLOCK_X_ADJUST,
                (RoomDimensions.Y / LevelConstants.BLOCKS_PER_ROOM_Y) * (gridIndex / LevelConstants.BLOCKS_PER_ROOM_X) + LevelConstants.BLOCK_X_ADJUST
            );
            Texture2D[] blockTextures = LoadBlockTextures();
            string token = tokens[tokenIndex];
            Block block = new Block(position, blockTextures, level);
            if (token.Contains("Load"))
            {
                block.loadRoom = new Vector2(
                    Int32.Parse(token.Substring(14, 1)),
                    Int32.Parse(token.Substring(16, 1))
                );
            }
            Blocks.Add(block);
            globalObjects.Add(block);
        }

        private void CreateItem(int tokenIndex, int gridIndex, List<string> tokens, List<IGameObject> globalObjects)
        {
            if (!Enum.TryParse(tokens[tokenIndex].Substring(LevelConstants.ITEM_START_INDEX), out ItemType itemType))
                return;
            Item item = new Item();
            item = item.CreateItem(itemType,
                (gridIndex % LevelConstants.BLOCKS_PER_ROOM_X) - LevelConstants.SHIFT_INTO_RANGE,
                (gridIndex / LevelConstants.BLOCKS_PER_ROOM_X) - LevelConstants.SHIFT_INTO_RANGE);
            Items.Add(item);
            globalObjects.Add(item);
        }

        public void Draw(SpriteBatch spriteBatch, Link link)
        {
            // Draw the room background.
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0,
                RoomWidth * LevelConstants.BACKGROUND_SIZE_SCALAR, RoomHeight * LevelConstants.BACKGROUND_SIZE_SCALAR), SourceRectangle, Color.White);

            // Draw the Link sprite.
            link.Draw(spriteBatch);

            // Draw room objects.
            foreach (var enemy in Enemies)
                enemy.DrawCurrentSprite(spriteBatch);
            foreach (var block in Blocks)
                block.Draw(spriteBatch);
            foreach (var item in Items)
                item.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime, List<IGameObject> globalObjects)
        {
            foreach (var enemy in Enemies) enemy.Update(gameTime, globalObjects);
            foreach (var block in Blocks) block.Update();
            foreach (var item in Items) item.Update(gameTime);
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(LevelConstants.LEVEL_CENTER_POSITION - 9, LevelConstants.LEVEL_CENTER_POSITION - 9,
                    (int)RoomDimensions.X - LevelConstants.LEVEL_CENTER_POSITION + 18,
                    (int)RoomDimensions.Y - LevelConstants.LEVEL_CENTER_POSITION + 18);
            }
        }
        public Vector2 Velocity
        {
            get { return new Vector2(0, 0); }
        }

        public void Destroy()
        {

        }
    }
}
