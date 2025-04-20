using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// the shader files are in Content folder with suffixes .fx, this is just C# code.

namespace MainGame.Shader
{
    public class ShaderManager
    {
        Effect hazeEffect;

        public ShaderManager(ContentManager content)
        {
            hazeEffect = content.Load<Effect>("hazeEffect");
        }
    }
}
