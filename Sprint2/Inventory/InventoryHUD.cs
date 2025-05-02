using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MainGame.Display;
using Zelda.Enums;
using System.Collections.Generic;

namespace Zelda.Inventory
{
    public class InventoryHUD
    {
        private Texture2D _backgroundTexture;
        private Texture2D pixel;
        private Link link;
        private LevelManager _level;

        // Data/coords for each sprite location in inv
        public struct SpriteDef
        {
            public float Scale;
            public Rectangle SourceRect;
            public int Width;
            public int Height;

            public SpriteDef(float scale, Rectangle sourceRect)
            {
                Scale = scale;
                SourceRect = sourceRect;
                Width = (int)(sourceRect.Width * scale);
                Height = (int)(sourceRect.Height * scale);
            }
        }

        private Dictionary<string, SpriteDef> _spriteDefs;

        public InventoryHUD(ContentManager content, GraphicsDevice graphicsDevice, Link _link, LevelManager level)
        {
            _backgroundTexture = content.Load<Texture2D>("PauseScreen");
            link = _link;
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            _level = level;
            InitializeSpriteDefinitions();
        }

        private void InitializeSpriteDefinitions()
        {
            _spriteDefs = new Dictionary<string, SpriteDef>();

            _spriteDefs["topLeft"] = new SpriteDef(InventoryConstants.TopLeftScale, InventoryConstants.TopLeftSourceRect);
            _spriteDefs["topRight"] = new SpriteDef(InventoryConstants.TopRightScale, InventoryConstants.TopRightSourceRect);
            _spriteDefs["dungeon"] = new SpriteDef(InventoryConstants.DungeonScale, InventoryConstants.DungeonSourceRect);

            _spriteDefs["compass"] = new SpriteDef(InventoryConstants.CompassScale, InventoryConstants.CompassSourceRect);
            _spriteDefs["mapIcon"] = new SpriteDef(InventoryConstants.MapIconScale, InventoryConstants.MapIconSourceRect);
            _spriteDefs["miniMap"] = new SpriteDef(InventoryConstants.MiniMapScale, InventoryConstants.MiniMapSourceRect);
            _spriteDefs["emptyMap"] = new SpriteDef(InventoryConstants.EmptyMapScale, InventoryConstants.EmptyMapSourceRect);

            _spriteDefs["arrowInv"] = new SpriteDef(InventoryConstants.ArrowInvScale, InventoryConstants.ArrowInvSourceRect);
            _spriteDefs["bombInv"] = new SpriteDef(InventoryConstants.BombInvScale, InventoryConstants.BombInvSourceRect);
            _spriteDefs["boomerangInv"] = new SpriteDef(InventoryConstants.BoomerangInvScale, InventoryConstants.BoomerangInvSourceRect);

            _spriteDefs["pinkIndicator"] = new SpriteDef(InventoryConstants.PinkIndicatorScale, InventoryConstants.PinkIndicatorSourceRect);

            _spriteDefs["selectedArrow"] = new SpriteDef(InventoryConstants.SelectedArrowScale, InventoryConstants.SelectedArrowSourceRect);
            _spriteDefs["selectedBomb"] = new SpriteDef(InventoryConstants.SelectedBombScale, InventoryConstants.SelectedBombSourceRect);
            _spriteDefs["selectedBoomerang"] = new SpriteDef(InventoryConstants.SelectedBoomerangScale, InventoryConstants.SelectedBoomerangSourceRect);
        }

        // Link tracking on minimap
        private int[] currentRoomMap()
        {
            int[] coords = _level.GetCurrentRoomCoords();
            int x = coords[0];
            int y = coords[1];

            return new int[] { InventoryConstants.BaseLX + InventoryConstants.LXOffset * x, InventoryConstants.BaseLY + InventoryConstants.LYOffset * y };
        }

        private Rectangle GetDestinationRect(SpriteDef sprite, int x, int y)
        {
            return new Rectangle(x, y, sprite.Width, sprite.Height);
        }

        public void DrawStaticElements(SpriteBatch spriteBatch)
        {
            // Draw black background rectangle.
            var viewport = spriteBatch.GraphicsDevice.Viewport;
            int halfWidth = viewport.Width + InventoryConstants.BackgroundExtraWidth;
            int halfHeight = viewport.Height + InventoryConstants.BackgroundExtraHeight;
            spriteBatch.Draw(
                pixel,
                new Rectangle(0, 0, halfWidth, halfHeight),
                Color.Black
            );

            spriteBatch.Draw(
                _backgroundTexture,
                GetDestinationRect(_spriteDefs["topLeft"], InventoryConstants.TopLeftDestX, InventoryConstants.TopLeftDestY),
                _spriteDefs["topLeft"].SourceRect,
                Color.White
            );

            spriteBatch.Draw(
                _backgroundTexture,
                GetDestinationRect(_spriteDefs["topRight"], InventoryConstants.TopRightDestX, InventoryConstants.TopRightDestY),
                _spriteDefs["topRight"].SourceRect,
                Color.White
            );

            spriteBatch.Draw(
                _backgroundTexture,
                GetDestinationRect(_spriteDefs["dungeon"], InventoryConstants.DungeonDestX, InventoryConstants.DungeonDestY),
                _spriteDefs["dungeon"].SourceRect,
                Color.White
            );
        }

        public void DrawDynamicElements(SpriteBatch spriteBatch)
        {
            if (link.CurrentItem.Contains(ItemType.Compass))
            {
                spriteBatch.Draw(
                    _backgroundTexture,
                    GetDestinationRect(_spriteDefs["compass"], InventoryConstants.CompassDestX, InventoryConstants.CompassDestY),
                    _spriteDefs["compass"].SourceRect,
                    Color.White
                );
            }

            if (link.CurrentItem.Contains(ItemType.Map))
            {
                // Map icon drawing
                spriteBatch.Draw(
                    _backgroundTexture,
                    GetDestinationRect(_spriteDefs["mapIcon"], InventoryConstants.MapIconDestX, InventoryConstants.MapIconDestY),
                    _spriteDefs["mapIcon"].SourceRect,
                    Color.White
                );

                // Full minimap
                spriteBatch.Draw(
                    _backgroundTexture,
                    GetDestinationRect(_spriteDefs["miniMap"], InventoryConstants.MiniMapDestX, InventoryConstants.MiniMapDestY),
                    _spriteDefs["miniMap"].SourceRect,
                    Color.White
                );
            }
            else // Draw empty map if no map item exists
            {
                spriteBatch.Draw(
                    _backgroundTexture,
                    GetDestinationRect(_spriteDefs["emptyMap"], InventoryConstants.EmptyMapDestX, InventoryConstants.EmptyMapDestY),
                    _spriteDefs["emptyMap"].SourceRect,
                    Color.White
                );
            }

            // Inventory icons
            if (link.CurrentItem.Contains(ItemType.Arrow))
            {
                spriteBatch.Draw(
                    _backgroundTexture,
                    GetDestinationRect(_spriteDefs["arrowInv"], InventoryConstants.ArrowInvDestX, InventoryConstants.ArrowInvDestY),
                    _spriteDefs["arrowInv"].SourceRect,
                    Color.White
                );
            }
            if (link.CurrentItem.Contains(ItemType.Bomb))
            {
                spriteBatch.Draw(
                    _backgroundTexture,
                    GetDestinationRect(_spriteDefs["bombInv"], InventoryConstants.BombInvDestX, InventoryConstants.BombInvDestY),
                    _spriteDefs["bombInv"].SourceRect,
                    Color.White
                );
            }
            if (link.CurrentItem.Contains(ItemType.Boomerang))
            {
                spriteBatch.Draw(
                    _backgroundTexture,
                    GetDestinationRect(_spriteDefs["boomerangInv"], InventoryConstants.BoomerangInvDestX, InventoryConstants.BoomerangInvDestY),
                    _spriteDefs["boomerangInv"].SourceRect,
                    Color.White
                );
            }

            // Minimap indicator
            if (link.CurrentItem.Contains(ItemType.Map))
            {
                int[] tempCoords = currentRoomMap();
                spriteBatch.Draw(
                    _backgroundTexture,
                    GetDestinationRect(_spriteDefs["pinkIndicator"], tempCoords[0], tempCoords[1]),
                    _spriteDefs["pinkIndicator"].SourceRect,
                    Color.White
                );
            }

            // Draw currently selected item in inventory (fixed destination)
            if (link.CurrentItem.Count > 0)
            {
                switch (link.CurrentItem[link.chooseItem])
                {
                    case ItemType.Arrow:
                        spriteBatch.Draw(
                            _backgroundTexture,
                            GetDestinationRect(_spriteDefs["selectedArrow"], InventoryConstants.SelectedItemDestX, InventoryConstants.SelectedItemDestY),
                            _spriteDefs["selectedArrow"].SourceRect,
                            Color.White
                        );
                        break;

                    case ItemType.Bomb:
                        spriteBatch.Draw(
                            _backgroundTexture,
                            GetDestinationRect(_spriteDefs["selectedBomb"], InventoryConstants.SelectedItemDestX, InventoryConstants.SelectedItemDestY),
                            _spriteDefs["selectedBomb"].SourceRect,
                            Color.White
                        );
                        break;

                    case ItemType.Boomerang:
                        spriteBatch.Draw(
                            _backgroundTexture,
                            GetDestinationRect(_spriteDefs["selectedBoomerang"], InventoryConstants.SelectedItemDestX, InventoryConstants.SelectedItemDestY),
                            _spriteDefs["selectedBoomerang"].SourceRect,
                            Color.White
                        );
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawStaticElements(spriteBatch);
            DrawDynamicElements(spriteBatch);
        }
    }
}
