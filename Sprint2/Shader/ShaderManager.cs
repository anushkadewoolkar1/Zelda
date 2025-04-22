using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// the shader files are in Content folder with suffixes .fx, this is just C# code.

namespace MainGame.Shader
{
    public class ShaderManager
    {
        private Effect hazeEffect;
        GraphicsDevice graphicsDevice;

        bool toggle = false;
        public ShaderManager(ContentManager content, GraphicsDevice graphicsDevice)
        {
            hazeEffect = content.Load<Effect>("hazeEffect");
            this.graphicsDevice = graphicsDevice;  
        }

        public void Toggle()
        {
            toggle = !toggle;
        }

        public void Update(GameTime gameTime)
        {
            hazeEffect.Parameters["Time"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);
            hazeEffect.Parameters["Resolution"].SetValue(new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height));
        }

        public void Draw(SpriteBatch spriteBatch, RenderTarget2D renderTarget)
        {

            if (toggle == true)
            {
                // draw with haze
                spriteBatch.Begin(
                    SpriteSortMode.Immediate,
                    BlendState.Opaque,
                    null, null, null,
                    hazeEffect
                );
                spriteBatch.Draw(
                    renderTarget,
                    new Rectangle(0, 0,
                        graphicsDevice.Viewport.Width,
                        graphicsDevice.Viewport.Height
                    ),
                    Color.White
                );
            }

            else
            {
                spriteBatch.Begin(
                    SpriteSortMode.Immediate,
                    BlendState.Opaque, 
                    null, null, null, null     
                );
                spriteBatch.Draw(
                    renderTarget,
                    new Rectangle(
                        0, 0,
                        graphicsDevice.Viewport.Width,
                        graphicsDevice.Viewport.Height
                    ),
                    Color.White
                );
                //spriteBatch.End();
            }
            
        }

    }
}
