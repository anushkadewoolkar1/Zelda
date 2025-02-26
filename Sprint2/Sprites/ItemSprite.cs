using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFactory;

namespace Sprint0.Sprites
{
    public class ItemSprite : ISprite
    {
        private Texture2D _texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private ItemSpriteFactory _factory;
        private String itemString;

        private Vector2 _position;

        private double elapsedTime = 0;
        private double frameDuration = 0.2;

        int _scale = 2;

        public ItemSprite(String spriteName, int posX, int posY)
        {
            _factory = ItemSpriteFactory.Instance;
            sourceRectangle = _factory.FetchItemSourceFromString(spriteName);
            _texture = _factory.GetTexture();
            itemString = _factory.GetItemStringFromIdx();
            itemString = spriteName;

            _position.X = posX;
            _position.Y = posY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle((int)_position.X, (int)_position.Y, sourceRectangle.Width * _scale, sourceRectangle.Height * _scale);

            spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void ItemCycleRight()
        {
            sourceRectangle = _factory.ItemCycleRightFactory();
            itemString = _factory.GetItemStringFromIdx();
        }

        public void ItemCycleLeft()
        {
            sourceRectangle = _factory.ItemCycleLeftFactory();
            itemString = _factory.GetItemStringFromIdx();
        }

        public string GetItemString()
        {
            return itemString;
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= frameDuration)
            {
                elapsedTime -= frameDuration;

                if (itemString.Contains("_frame_000"))
                {
                    itemString = itemString.Replace("_frame_000", "_frame_001");
                }
                else if (itemString.Contains("_frame_001"))
                {
                    itemString = itemString.Replace("_frame_001", "_frame_000");
                }
                sourceRectangle = _factory.FetchItemSourceFromString(itemString);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {

        }
    }
}
