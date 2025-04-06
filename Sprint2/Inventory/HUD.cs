using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sprint0.CollisionHandling;
using Zelda.Enums;
using static System.Formats.Asn1.AsnWriter;


namespace ZeldaGame.HUD
{
    public class HUD
    {
        private Texture2D _backgroundTexture;
        private GameState _gameState;

        public HUD(ContentManager content)
        {
            _backgroundTexture = content.Load<Texture2D>("PauseScreen");
        }

        public void UpdateGameState(GameState gameState)
        {
            _gameState = gameState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_gameState != GameState.Playing) return;

            // life and A/B button stuff at the bottom
            float scale = 1.8f;
            Rectangle bottomSource = new Rectangle(344, 17, 154, 44);
            int bottomWidth = (int)(bottomSource.Width * scale);
            int bottomHeight = (int)(bottomSource.Height * scale);

            spriteBatch.Draw(
                _backgroundTexture,
                new Rectangle(135, 360, bottomWidth, bottomHeight),
                bottomSource,
                Color.White
            );
        }
    }
}
