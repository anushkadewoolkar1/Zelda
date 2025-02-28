using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Zelda.CollisionMap
{
    public class TileMap
    {
        private static TileMap instance;
        private int[,] tileMap;
        private float tilePixelWidth;
        private float tilePixelHeight;
        private int tilesWidth = 12, tilesHeight = 7;

        private TileMap(int screenWidth, int screenHeight)
        {
            tilePixelWidth = screenWidth / (float)tilesWidth;
            tilePixelHeight = screenHeight / (float)tilesHeight;
            tileMap = new int[tilesWidth, tilesHeight];
        }

        public static void Initialize(int screenWidth, int screenHeight)
        {
            if (instance == null)
            {
                instance = new TileMap(screenWidth, screenHeight);
            }
            else
            {
                throw new InvalidOperationException("TileMap already init and its singleton.");
            }
        }

        public static TileMap GetInstance()
        {
            if (instance == null)
            {
                throw new InvalidOperationException("do TileMap.Initialize before TileMap.GetInstance() its not initialized yet.");
            }
            return instance;
        }

        public void SetTileCollision(int x, int y, int collidable)
        {
            if ((x >= 0 && x < tilesWidth) && (y >= 0 && y < tilesHeight))
            {
                tileMap[x, y] = collidable;
            }
        }

        public bool CheckCollision(int x, int y)
        {
            return tileMap[x, y] == 1;
        }

        public Vector2 GetTileCenter(Vector2 tilePosition)
        {
            return new Vector2(
                tilePosition.X * tilePixelWidth + tilePixelWidth / 2f,
                tilePosition.Y * tilePixelHeight + tilePixelHeight / 2f
            );
        }

    }
}