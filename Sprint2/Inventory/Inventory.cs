using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sprint0.CollisionHandling;
using Zelda.Enums;

namespace Zelda.Inventory
{
    public class Inventory
    {
        private Texture2D _backgroundTexture;
        private List<ItemType> pickedUpItems;

        private Texture2D pixel;

        private Link link;
        private ItemType currentItem;

        public Inventory(ContentManager content, GraphicsDevice graphicsDevice, Link _link)
        {
            _backgroundTexture = content.Load<Texture2D>("PauseScreen");
            pickedUpItems = _link.CurrentItem;
            link = _link;
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void OpenInventory()
        {
            if (link.chooseItem != null)
            {
                currentItem = link.CurrentItem[link.chooseItem];
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            // black screen replace zelda game background first
            var viewport = spriteBatch.GraphicsDevice.Viewport;
            int halfWidth = viewport.Width / 3 + 33;
            int halfHeight = viewport.Height / 3 - 8;

            // Draw black rectangle over 
            spriteBatch.Draw(
                pixel,
                new Rectangle(0, 0, halfWidth, halfHeight),
                Color.Black
            );




            // Inventory + square under it + "use b button for this"
            float scale = 1.5f;
            Rectangle topLeftSource = new Rectangle(8, 20, 108, 78);
            int topLeftSourceWidth = (int)(topLeftSource.Width * scale);
            int topLeftSourceHeight = (int)(topLeftSource.Height * scale);

            spriteBatch.Draw(
                _backgroundTexture,
                new Rectangle(30, 5, topLeftSourceWidth, topLeftSourceHeight), // 10 pixels below top
                topLeftSource,
                Color.White
            );


            // blue rectangle to the right of the use b button for this
            scale = 1.64f;
            Rectangle topRightSource = new Rectangle(115, 50, 130, 48);
            int topRightSourceWidth = (int)(topRightSource.Width * scale);
            int topRightSourceHeight = (int)(topRightSource.Height * scale);

            spriteBatch.Draw(
                _backgroundTexture,
                new Rectangle(240, 53, topRightSourceWidth, topRightSourceHeight),
                topRightSource,
                Color.White
                );

            // Map/Compass and minimap
            scale = 1.8f;
            Rectangle dungeonSource = new Rectangle(260, 114, 252, 84);
            int dungeonWidth = (int)(dungeonSource.Width * scale);
            int dungeonHeight = (int)(dungeonSource.Height * scale);

            spriteBatch.Draw(
                _backgroundTexture,
                new Rectangle(25, 135, dungeonWidth, dungeonHeight),
                dungeonSource,
                Color.White
            );


            // life and A/B button stuff at the bottom
            scale = 1.5f;
            Rectangle bottomSource = new Rectangle(344, 17, 154, 44);
            int bottomWidth = (int)(bottomSource.Width * scale);
            int bottomHeight = (int)(bottomSource.Height * scale);

            spriteBatch.Draw(
                _backgroundTexture,
                new Rectangle(230, 280, bottomWidth, bottomHeight),
                bottomSource,
                Color.White
            );

            // draw compass if pick up
            if (link.CurrentItem.Contains(ItemType.Compass))
            {
                scale = 2.5f;
                Rectangle compassSource = new Rectangle(612, 157, 14, 14);
                int compassWidth = (int)(compassSource.Width * scale);
                int compassHeight = (int)(compassSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(95, 245, compassWidth, compassHeight),
                    compassSource,
                    Color.White
                );
            }

            // draw map if pick up
            if (link.CurrentItem.Contains(ItemType.Map))
            {
                scale = 2.4f;
                Rectangle mapSource = new Rectangle(602, 157, 6, 14);
                int mapWidth = (int)(mapSource.Width * scale);
                int mapHeight = (int)(mapSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(105, 175, mapWidth, mapHeight),
                    mapSource,
                    Color.White
                );
            }
        }

    }
}
