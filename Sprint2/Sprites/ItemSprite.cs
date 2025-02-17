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
        private ItemSpriteFactory factory;
        private String itemString;

        public ItemSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int width, int height, String spriteName)
        {
            _texture = texture;
            sourceRectangle = new Rectangle(spriteSheetXPos,  spriteSheetYPos, width, height);
            factory = ItemSpriteFactory.Instance;
            itemString = factory.getItemStringFromIdx();
            itemString = spriteName;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);

            spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void itemCycleRight()
        {
            sourceRectangle = factory.itemCycleRightFactory();
            itemString = factory.getItemStringFromIdx();
        }

        public void itemCycleLeft()
        {
            sourceRectangle = factory.itemCycleLeftFactory();
            itemString = factory.getItemStringFromIdx();
        }

        public string getItemString()
        {
            return itemString;
        }

        public void Update(GameTime gameTime)
        {
            //...
        }

        public void Draw(SpriteBatch _textures)
        {
            //...
        }
    }
}
