using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.Sprites;

namespace MainGame.States
{
    public interface IEnemyState
    {
        void Load();
        void Update(GameTime gameTime);
        void Stop();
    }
}
