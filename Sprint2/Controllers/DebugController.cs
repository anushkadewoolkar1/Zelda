using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Sprint0.Commands;
using Sprint0.ILevel;

namespace Sprint0.Controllers
{
    public abstract class DebugController : IController
    {
        public void Update() { }
        public abstract void Update(Level level);
    }
}
