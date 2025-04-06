using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.Display;

namespace MainGame.Controllers
{
    // Interface for controllers
    public interface IController
    {
        // Update is called once per frame to handle input
        public void Update();
    }
}
