using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using Zelda.Inventory;  // Gives us access to Inventory.SpriteDef
using MainGame.Display;
using System.Collections.Generic;

namespace ZeldaGame.HUD
{
    public class HUD
    {
        private Texture2D _backgroundTexture;
        private GameState _gameState;
        private Link _link;

        private Dictionary<string, Inventory.SpriteDef> _spriteDefs;

        public HUD(ContentManager content, Link link)
        {
            _backgroundTexture = content.Load<Texture2D>("PauseScreen");
            _link = link;
            InitializeSpriteDefinitions();
        }

        private void InitializeSpriteDefinitions()
        {
            _spriteDefs = new Dictionary<string, Inventory.SpriteDef>();

            _spriteDefs["bottomBar"] = new Inventory.SpriteDef(HudConstants.BottomBarScale, HudConstants.BottomBarSourceRect);

            _spriteDefs["fullHeart"] = new Inventory.SpriteDef(HudConstants.HeartIconScale, HudConstants.FullHeartSourceRect);
            _spriteDefs["emptyHeart"] = new Inventory.SpriteDef(HudConstants.HeartIconScale, HudConstants.EmptyHeartSourceRect);


            _spriteDefs["miniMapMap"] = new Inventory.SpriteDef(HudConstants.MiniMapScale, HudConstants.BottomMiniMapSourceRect_Map);
            _spriteDefs["miniMapEmpty"] = new Inventory.SpriteDef(HudConstants.MiniMapScale, HudConstants.BottomMiniMapSourceRect_Empty);

            _spriteDefs["selectedArrow"] = new Inventory.SpriteDef(HudConstants.SelectedArrowScale, HudConstants.SelectedArrowSourceRect);
            _spriteDefs["selectedBomb"] = new Inventory.SpriteDef(HudConstants.SelectedBombScale, HudConstants.SelectedBombSourceRect);
            _spriteDefs["selectedBoomerang"] = new Inventory.SpriteDef(HudConstants.SelectedBoomerangScale, HudConstants.SelectedBoomerangSourceRect);
        }


        private Rectangle GetDestinationRect(Inventory.SpriteDef sprite, int x, int y)
        {
            return new Rectangle(x, y, sprite.Width, sprite.Height);
        }

        public void UpdateGameState(GameState gameState)
        {
            _gameState = gameState;
        }

        public void DrawStaticElements(SpriteBatch spriteBatch)
        {
            Inventory.SpriteDef bottomBar = _spriteDefs["bottomBar"];
            Rectangle bottomBarDest = GetDestinationRect(bottomBar, HudConstants.BottomBarDestX, HudConstants.BottomBarDestY);
            spriteBatch.Draw(_backgroundTexture, bottomBarDest, bottomBar.SourceRect, Color.White);

            // hearts
            int health = _link.Health;
            Inventory.SpriteDef fullHeart = _spriteDefs["fullHeart"];
            Inventory.SpriteDef emptyHeart = _spriteDefs["emptyHeart"];

            for (int i = 0; i < 3; i++)
            {
                int x = HudConstants.HeartStartX + HudConstants.HeartSpacing * i;
                Rectangle heartDest = new Rectangle(x, HudConstants.HeartY, fullHeart.Width, fullHeart.Height);

                if (i < health)
                {
                    spriteBatch.Draw(_backgroundTexture, heartDest, fullHeart.SourceRect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(_backgroundTexture, heartDest, emptyHeart.SourceRect, Color.White);
                }
            }
        }

        public void DrawDynamicElements(SpriteBatch spriteBatch)
        {
            List<ItemType> currItem = _link.CurrentItem;
            int chosenItem = _link.chooseItem;
            // minimap @ bottom
            if (_link.CurrentItem.Contains(ItemType.Map))
            {
                Inventory.SpriteDef miniMap = _spriteDefs["miniMapMap"];
                Rectangle miniMapDest = GetDestinationRect(miniMap, HudConstants.MiniMapDestX, HudConstants.MiniMapDestY);
                spriteBatch.Draw(_backgroundTexture, miniMapDest, miniMap.SourceRect, Color.White);
            }
            else
            {
                Inventory.SpriteDef miniMapEmpty = _spriteDefs["miniMapEmpty"];
                Rectangle miniMapDest = GetDestinationRect(miniMapEmpty, HudConstants.MiniMapDestX, HudConstants.MiniMapDestY);
                spriteBatch.Draw(_backgroundTexture, miniMapDest, miniMapEmpty.SourceRect, Color.White);
            }

            // draw inv item selected
            if (currItem.Count > 0)
            {
                switch (currItem[chosenItem])
                {
                    case ItemType.Arrow:
                        {
                            Inventory.SpriteDef selectedArrow = _spriteDefs["selectedArrow"];
                            Rectangle arrowDest = GetDestinationRect(selectedArrow, HudConstants.SelectedArrowDestX, HudConstants.SelectedArrowDestY);
                            spriteBatch.Draw(_backgroundTexture, arrowDest, selectedArrow.SourceRect, Color.White);
                            break;
                        }
                    case ItemType.Bomb:
                        {
                            Inventory.SpriteDef selectedBomb = _spriteDefs["selectedBomb"];
                            Rectangle bombDest = GetDestinationRect(selectedBomb, HudConstants.SelectedBombDestX, HudConstants.SelectedBombDestY);
                            spriteBatch.Draw(_backgroundTexture, bombDest, selectedBomb.SourceRect, Color.White);
                            break;
                        }
                    case ItemType.Boomerang:
                        {
                            Inventory.SpriteDef selectedBoomerang = _spriteDefs["selectedBoomerang"];
                            Rectangle boomerangDest = GetDestinationRect(selectedBoomerang, HudConstants.SelectedBoomerangDestX, HudConstants.SelectedBoomerangDestY);
                            spriteBatch.Draw(_backgroundTexture, boomerangDest, selectedBoomerang.SourceRect, Color.White);
                            break;
                        }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_gameState != GameState.Playing) return;

            DrawStaticElements(spriteBatch);
            DrawDynamicElements(spriteBatch);
        }
    }
}
