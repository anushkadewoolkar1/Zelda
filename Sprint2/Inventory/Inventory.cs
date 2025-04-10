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
using MainGame.Display;

namespace Zelda.Inventory
{
    public class Inventory
    {
        private Texture2D _backgroundTexture;

        private Texture2D pixel;

        private Link link;

        private LevelManager _level;

        public Inventory(ContentManager content, GraphicsDevice graphicsDevice, Link _link, LevelManager level)
        {
            _backgroundTexture = content.Load<Texture2D>("PauseScreen");
            link = _link;
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            _level = level;
        }

        private int[] currentRoomMap()
        {
            switch (_level.currentRoomCoords[0], _level.currentRoomCoords[1])
            {
               
                case (1, 5): return new int[] { 307, 271 };
                case (1, 3): return new int[] { 307, 242 };
                case (1, 2): return new int[] { 307, 227 };
                case (1, 0): return new int[] { 307, 197 };

                case (3, 5): return new int[] { 336, 271 };
                case (3, 3): return new int[] { 336, 242 };
                case (3, 2): return new int[] { 336, 227 };

                case (2, 5): return new int[] { 322, 271 };
                case (2, 4): return new int[] { 322, 256 };
                case (2, 3): return new int[] { 322, 242 };
                case (2, 2): return new int[] { 322, 227 };
                case (2, 1): return new int[] { 322, 212 };
                case (2, 0): return new int[] { 322, 197 };

                case (4, 2): return new int[] { 351, 227 };
                case (4, 1): return new int[] { 351, 212 };

                case (5, 1): return new int[] { 365, 212 };
            }
            return new int[] { 300, 300 };
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            // black screen replace zelda game background first
            var viewport = spriteBatch.GraphicsDevice.Viewport;
            int halfWidth = viewport.Width / 1 + 33;
            int halfHeight = viewport.Height / 1 - 8;

            // Draw black rectangle over 
            spriteBatch.Draw(
                pixel,
                new Rectangle(0, 0, halfWidth, halfHeight),
                Color.Black
            );




            // Inventory + square under it + "use b button for this"
            float scale = 1.5f;
            Rectangle topLeftSource = new Rectangle(8, 20, 108, 78);
            int topLeftSourceWidth = (int)(topLeftSource.Width * scale);
            int topLeftSourceHeight = (int)(topLeftSource.Height * scale);

            spriteBatch.Draw(
                _backgroundTexture,
                new Rectangle(30, 5, topLeftSourceWidth, topLeftSourceHeight), // 10 pixels below top
                topLeftSource,
                Color.White
            );


            // blue rectangle to the right of the use b button for this
            scale = 1.64f;
            Rectangle topRightSource = new Rectangle(115, 50, 130, 48);
            int topRightSourceWidth = (int)(topRightSource.Width * scale);
            int topRightSourceHeight = (int)(topRightSource.Height * scale);

            spriteBatch.Draw(
                _backgroundTexture,
                new Rectangle(240, 53, topRightSourceWidth, topRightSourceHeight),
                topRightSource,
                Color.White
                );

            // Map/Compass and minimap
            scale = 1.8f;
            Rectangle dungeonSource = new Rectangle(260, 114, 83, 84);
            int dungeonWidth = (int)(dungeonSource.Width * scale);
            int dungeonHeight = (int)(dungeonSource.Height * scale);

            spriteBatch.Draw(
                _backgroundTexture,
                new Rectangle(25, 165, dungeonWidth, dungeonHeight),
                dungeonSource,
                Color.White
            );

            // draw compass if pick up
            if (link.CurrentItem.Contains(ItemType.Compass))
            {
                scale = 2.5f;
                Rectangle compassSource = new Rectangle(612, 157, 14, 14);
                int compassWidth = (int)(compassSource.Width * scale);
                int compassHeight = (int)(compassSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(95, 265, compassWidth, compassHeight),
                    compassSource,
                    Color.White
                );
            }

            // draw map and minimap if pick up
            if (link.CurrentItem.Contains(ItemType.Map))
            {

                // map icon
                scale = 2.4f;
                Rectangle mapSource = new Rectangle(602, 157, 6, 14);
                int mapWidth = (int)(mapSource.Width * scale);
                int mapHeight = (int)(mapSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(105, 195, mapWidth, mapHeight),
                    mapSource,
                    Color.White
                );

                // map in the middle
                scale = 1.8f;
                Rectangle middleMiniMapSource = new Rectangle(345, 114, 150, 84);
                int middleMiniMapWidth = (int)(middleMiniMapSource.Width * scale);
                int middleMiniMapHeight = (int)(middleMiniMapSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(200, 180, middleMiniMapWidth, middleMiniMapHeight),
                    middleMiniMapSource,
                    Color.White
                );
            }
            // otherwise, draw the blank map
            else
            {
                scale = 1.8f;
                Rectangle emptyMapSource = new Rectangle(348, 214, 125, 80);
                int emptyMapWidth = (int)(emptyMapSource.Width * scale);
                int emptyMapHeight = (int)(emptyMapSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(200, 180, emptyMapWidth, emptyMapHeight),
                    emptyMapSource,
                    Color.White
                );
            }

            // arrow 
            if (link.CurrentItem.Contains(ItemType.Arrow))
            {
                scale = 2.6f;
                Rectangle arrowSource = new Rectangle(617, 138, 5, 13);
                int arrowWidth = (int)(arrowSource.Width * scale);
                int arrowHeight = (int)(arrowSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(273, 75, arrowWidth, arrowHeight),
                    arrowSource,
                    Color.White
                 );
            }


            // bomb
            if (link.CurrentItem.Contains(ItemType.Bomb))
            {
                scale = 2.5f;
                Rectangle bombSource = new Rectangle(604, 138, 8, 14);
                int bombWidth = (int)(bombSource.Width * scale);
                int bombHeight = (int)(bombSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(310, 75, bombWidth, bombHeight),
                    bombSource,
                    Color.White
                );
            }

            if (link.CurrentItem.Contains(ItemType.Boomerang))
            {
                scale = 2.5f;
                Rectangle boomerangSource = new Rectangle(585, 138, 7, 14);
                int bombWidth = (int)(boomerangSource.Width * scale);
                int bombHeight = (int)(boomerangSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(360, 79, bombWidth, bombHeight),
                    boomerangSource,
                    Color.White
                );
            }

            if (link.CurrentItem.Contains(ItemType.Map))
            {

                int[] tempCoords = currentRoomMap();
                scale = 1.0f;
                // minimap link tracker in inventory
                Rectangle pinkSource = new Rectangle(350, 70, 5, 5);
                int pinkWidth = (int)(pinkSource.Width * scale);
                int pinkHeight = (int)(pinkSource.Height * scale);

                spriteBatch.Draw(
                    _backgroundTexture,
                    new Rectangle(tempCoords[0], tempCoords[1], pinkWidth, pinkHeight),
                    pinkSource,
                    Color.White
                );
            }


            if (link.CurrentItem.Count > 0)
            {
                switch (link.CurrentItem[link.chooseItem])
                {
                    case ItemType.Arrow:

                        scale = 2.3f;
                        Rectangle arrowSource = new Rectangle(617, 138, 5, 13);
                        int arrowWidth = (int)(arrowSource.Width * scale);
                        int arrowHeight = (int)(arrowSource.Height * scale);

                        spriteBatch.Draw(
                            _backgroundTexture,
                            new Rectangle(121, 59, arrowWidth, arrowHeight),
                            arrowSource,
                            Color.White
                         );
                        break;

                    case ItemType.Bomb:

                        scale = 2.2f;
                        Rectangle bombSource = new Rectangle(604, 138, 8, 14);
                        int bombWidth = (int)(bombSource.Width * scale);
                        int bombHeight = (int)(bombSource.Height * scale);

                        spriteBatch.Draw(
                            _backgroundTexture,
                            new Rectangle(121, 59, bombWidth, bombHeight),
                            bombSource,
                            Color.White
                        );
                        break;

                    case ItemType.Boomerang:

                        scale = 2.4f;
                        Rectangle boomerangSource = new Rectangle(585, 140, 7, 12);
                        int boomerangWidth = (int)(boomerangSource.Width * scale);
                        int boomerangHeight = (int)(boomerangSource.Height * scale);

                        spriteBatch.Draw(
                            _backgroundTexture,
                            new Rectangle(121, 59, boomerangWidth, boomerangHeight),
                            boomerangSource,
                            Color.White
                        );
                        break;
                }
            }

        }

    }
}
