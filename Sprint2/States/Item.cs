using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using Sprint0.Sprites;
using Zelda.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFactory;
using ZeldaGame.Zelda.CollisionMap;
using Sprint0.CollisionHandling;

namespace Sprint0.States
{
    public class Item : IGameObject
    {
        public ItemType itemType;
        public ItemSprite itemSprite;
        public Vector2 position;
        public Vector2 pixelPosition;

        TileMap tileMap = TileMap.GetInstance();
        private Vector2 tilePosition;
        public Item()
        {

        }

        public Item CreateItem(ItemType itemType, int posX, int posY)
        {
            Item newItem;

            switch (itemType)
            {
                case ItemType.Arrow:

                    newItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteArrow", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;

                default:
                    throw new ArgumentException("Invalid item type", nameof(itemType));
            }

            return newItem ?? throw new InvalidOperationException("Item creation failed.");
        }

        public ItemSprite GetItemSprite()
        {
            return itemSprite;
        }

        public void Update(GameTime gameTime)
        {
            itemSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pixelPosition = tileMap.GetTileCenter(tilePosition);
            itemSprite.Draw(spriteBatch, pixelPosition);
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)pixelPosition.X, (int)pixelPosition.Y, 14, 14);
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return new Vector2(0, 0);
            }
        }


    }
}
