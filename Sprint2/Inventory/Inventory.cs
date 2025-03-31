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

        public Inventory(ContentManager content, Link link)
        {
            _backgroundTexture = content.Load<Texture2D>("PauseScreen");
            pickedUpItems = link.CurrentItem;
        }

        public void OpenInventory()
        {
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, 800, 480), Color.White);
        }
    }
}
