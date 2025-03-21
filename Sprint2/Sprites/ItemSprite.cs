using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        private const string START_FRAME = "_frame_000";
        private const string ALT_FRAME = "_frame_001";

        private Vector2 _position;

        private double elapsedTime = 0;
        private double frameDuration = 0.2;

        float _scale = 1.7f;

        bool pickedUp = false;

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

        public void Draw(SpriteBatch spriteBatch, Vector2 pixelPosition)
        {
            // destinationRectangle = new Rectangle((int)_position.X, (int)_position.Y, sourceRectangle.Width * _scale, sourceRectangle.Height * _scale);
            destinationRectangle = new Rectangle((int)pixelPosition.X, (int)pixelPosition.Y, (int)(sourceRectangle.Width * _scale), (int)(sourceRectangle.Height * _scale));

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

        public void SetPickedUp()
        {
            pickedUp = true;
        }

        public bool GetPickedUp()
        {
            return pickedUp;
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= frameDuration)
            {
                elapsedTime -= frameDuration;

                if (itemString.Contains(START_FRAME))
                {
                    itemString = itemString.Replace(START_FRAME, ALT_FRAME);
                }
                else if (itemString.Contains(ALT_FRAME))
                {
                    itemString = itemString.Replace(ALT_FRAME, START_FRAME);
                }
                sourceRectangle = _factory.FetchItemSourceFromString(itemString);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime, Link link)
        {
            //no-op
        }
    }
}
