﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MainGame.Commands;
using MainGame.Display;
using Zelda.Enums;
// i comment this out cuz it was throwing me an error and didnt build
namespace MainGame.Display
{
    //
    public class StartMenu : BaseMenu
    {
        private Texture2D backgroundTexture;
        private GameState GameState;
        private GameAudio audio = GameAudio.Instance;

        public StartMenu(ContentManager content)
        {
            backgroundTexture = content.Load<Texture2D>("LegendOfZeldaStartScreen");

            GameState = Zelda.Enums.GameState.StartMenu;
        }

        // Called once per frame to update sprites
        public override void Update(GameTime gameTime)
        {
        }

        // Draws sprite on the screen
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (GameState != Zelda.Enums.GameState.StartMenu) return;
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 512, 480), Color.White);
        }

        public override void LoadCommand(ICommand command)
        {
            //no-op
        }

        public void PauseLevel(LevelManager level)
        {
            //no-op
        }

        public void LeaveMenu()
        {
            GameState = GameState.Playing;
        }

        public override void UpdateGameState(GameState _gameState)
        {
            GameState = _gameState;
            if (GameState == GameState.StartMenu && MediaPlayer.State != MediaState.Playing)
            {
                audio.PlayTitleBGM();
            }
        }
    }
}
