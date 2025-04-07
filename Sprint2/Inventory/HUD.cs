using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MainGame.CollisionHandling;
using Zelda.Enums;
using static System.Formats.Asn1.AsnWriter;


namespace ZeldaGame.HUD
{
    public class HUD
    {
        private Texture2D _backgroundTexture;
        private GameState _gameState;

        private Link _link;

        public HUD(ContentManager content, Link link)
        {
            _backgroundTexture = content.Load<Texture2D>("PauseScreen");
            _link = link;
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

            //health
            int health = _link.Health;
            scale = 2.5f;
            Rectangle fullHeartSource = new Rectangle(645, 117, 8, 8);
            int fullHeartWidth = (int)(fullHeartSource.Width * scale);
            int fullHeartHeight = (int)(fullHeartSource.Height * scale);

            Rectangle emptyHeartSource = new Rectangle(627, 117, 8, 8);
            int emptyHeartWidth = (int)(emptyHeartSource.Width * scale);
            int emptyHeartHeight = (int)(emptyHeartSource.Height * scale);

            // full minimap bottom left
            if (_link.CurrentItem.Contains(ItemType.Map))
            {
                scale = 1.8f;
                Rectangle bottomMiniMapSource = new Rectangle(584, 0, 62, 38);
                int bottomMiniMapWidth = (int)(bottomMiniMapSource.Width * scale);
                int bottomMiniMapHeight = (int)(bottomMiniMapSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(10, 360, bottomMiniMapWidth, bottomMiniMapHeight),
                    bottomMiniMapSource,
                    Color.White
                );
            }
            // empty bottom left minimap
            else
            {
                scale = 1.8f;
                Rectangle bottomMiniMapSource = new Rectangle(754, 0, 70, 50);
                int bottomMiniMapWidth = (int)(bottomMiniMapSource.Width * scale);
                int bottomMiniMapHeight = (int)(bottomMiniMapSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(10, 360, bottomMiniMapWidth, bottomMiniMapHeight),
                    bottomMiniMapSource,
                    Color.White
                );
            }
            

            for (int i = 0; i <= 2; i++)
            {

                if (i < health)
                {
                    spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(320 + 30 * i, 405, fullHeartWidth, fullHeartHeight),
                    fullHeartSource,
                    Color.White
                );

                }

                if (i >= health)
                {
                    spriteBatch.Draw(
                        _backgroundTexture,
                        new Rectangle(320 + 30 * i, 405, emptyHeartWidth, emptyHeartHeight),
                        emptyHeartSource,
                        Color.White
                    );
                }
            }
        }
    }
}
