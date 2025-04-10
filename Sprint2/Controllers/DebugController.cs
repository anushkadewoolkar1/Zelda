using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using MainGame.Commands;
using MainGame.Display;

namespace MainGame.Controllers
{
    public abstract class DebugController : IController
    {
        public void Update() { }
        public abstract void Update(LevelManager level);
    }
}
