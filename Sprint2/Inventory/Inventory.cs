using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.CollisionHandling;

namespace ZeldaGame.Zelda.Sprint2.Inventory
{
    public class Inventory
    {
        private Texture2D _backgroundTexture;

        public Inventory(ContentManager content, List<IGameObject> gameObjects)
        {
            _backgroundTexture = content.Load<Texture2D>("PauseScreen");
        }
    }
}
