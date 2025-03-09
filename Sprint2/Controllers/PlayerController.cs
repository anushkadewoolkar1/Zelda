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
    public abstract class PlayerController : IController
    {
        public abstract void Update();
    }
}
