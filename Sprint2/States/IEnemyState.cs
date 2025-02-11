using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.States
{
    internal interface IEnemyState
    {
        void Load();
        void Update(GameTime gameTime);
    }
}
