using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Sprint0.Commands;

namespace Sprint0.Controllers
{
    public class MouseController : DebugController
    {
        private int _windowWidth;
        private int _windowHeight;

        public MouseController(int windowWidth, int windowHeight)
        {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public void Update()
        {
            MouseState state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed)
            {
                int mouseX = state.X;
                int mouseY = state.Y;

                bool topHalf = mouseY < _windowHeight / 2;
                bool leftHalf = mouseX < _windowWidth / 2;
        }
    }
}