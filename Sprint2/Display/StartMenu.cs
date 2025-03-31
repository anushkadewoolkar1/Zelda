using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint0.Commands;
using Sprint0.Display;
using Zelda.Enums;
// i comment this out cuz it was throwing me an error and didnt build
namespace Sprint0.Display
{
    //
    public class StartMenu : IMenu
    {
        private Texture2D backgroundTexture;
        private GameState GameState;
        private GameAudio _audio;

        public StartMenu(ContentManager content)
        {
            backgroundTexture = content.Load<Texture2D>("LegendOfZeldaStartScreen");

            GameState = Zelda.Enums.GameState.StartMenu;

            _audio = GameAudio.Instance;
        }

        // Called once per frame to update sprites
        public void Update(GameTime gameTime)
        {
        }

        // Draws sprite on the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameState != Zelda.Enums.GameState.StartMenu) return;
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1440, 1080), Color.White);
        }

        public void LoadCommand(ICommand command)
        {
            //no-op
        }

        public void PauseLevel(Level level)
        {
            //no-op
        }

        public void LeaveMenu()
        {
            GameState = GameState.Playing;
        }

        public void UpdateGameState(GameState _gameState)
        {
            GameState = _gameState;
            if (GameState == GameState.StartMenu && MediaPlayer.State != MediaState.Playing)
            {
                _audio.PlayTitleBGM();
            }
        }
    }
}
