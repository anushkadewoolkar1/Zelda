using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MainGame.Visibility;
using MainGame.Sprites;
using Zelda.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFactory;
using ZeldaGame.Zelda.CollisionMap;
using MainGame.CollisionHandling;

namespace MainGame.States
{
    public class Item : IGameObject
    {
        public Item currItem;
        public ItemType itemType;
        public ItemSprite itemSprite;
        public Vector2 position { get; set; }
        public Vector2 pixelPosition;

        public bool isVisible = true;
        private FogOfWar fow = FogOfWar.Instance;

        TileMap tileMap = TileMap.GetInstance();
        private Vector2 tilePosition;
        public Item()
        {

        }

        public Item CreateItem(ItemType itemType, int posX, int posY)
        {

            switch (itemType)
            {
                case ItemType.None:
                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("transparent_block", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;

                case ItemType.Arrow:

                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteArrow", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;

                case ItemType.Bomb:
                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteBomb", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;


                case ItemType.Boomerang:
                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteBoomerang", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;

                case ItemType.Compass:
                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteCompass", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;

                    // this below doesnt work
                case ItemType.Fireball:
                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteFireball", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;

                case ItemType.Key:
                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteKey", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;
                case ItemType.Map:
                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteMap", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;
                case ItemType.Triforce:
                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteTriforce_frame_000", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;

                case ItemType.Bow:
                    currItem = new Item
                    {
                        itemSprite = new ItemSprite("ZeldaSpriteBow", posX, posY),
                        position = new Vector2 { X = posX, Y = posY },
                        tilePosition = new Vector2 { X = posX, Y = posY }
                    };
                    break;

                default:
                    throw new ArgumentException("Invalid item type", nameof(itemType));
            }

            currItem.itemType = itemType;

            return currItem ?? throw new InvalidOperationException("Item creation failed.");
        }

        // Item should disappear after being picked up (PP):
        public void Destroy()
        {
            tilePosition.X -= 20;
            isVisible = true;
        }

        public ItemSprite GetItemSprite()
        {
            return itemSprite;
        }

        public void Update(GameTime gameTime)
        {
            if (!isVisible) return;
            itemSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)

        {
            if (!isVisible || !fow.FogOfWarCheck((IGameObject)this)) return;
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
