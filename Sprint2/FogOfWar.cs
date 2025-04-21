using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.CollisionHandling;
using MainGame.States;
using MainGame.Sprites;
using Microsoft.Xna.Framework;
using Zelda.Enums;

namespace MainGame.Visibility
{

    public class FogOfWar
    {
        private static FogOfWar instance = new FogOfWar();

        private bool fogOfWarToggle;
        private Direction linkFacingDirection;
        private Vector2 linkPosition;

        public static FogOfWar Instance
        {
            get
            {
                return instance;
            }
        }

        private FogOfWar()
        {
            fogOfWarToggle = true;
        }

        public Boolean FogOfWarCheck(IGameObject gameObject)
        {
            if (!fogOfWarToggle) return true;

            Boolean Result = false;

            if ((gameObject as Enemy) != null)
            {
                Enemy enemy = (Enemy)gameObject;
                switch (linkFacingDirection)
                {
                    case Direction.Left:
                        if (enemy.position.X <= linkPosition.X)
                        {
                            Result = true;
                        }
                        break;
                    case Direction.Up:
                        if (enemy.position.Y <= linkPosition.Y)
                        {
                            Result = true;
                        }
                        break;
                    case Direction.Right:
                        if (enemy.position.X >= linkPosition.X)
                        {
                            Result = true;
                        }
                        break;
                    case Direction.Down:
                        if (enemy.position.Y >= linkPosition.Y)
                        {
                            Result = true;
                        }
                        break;
                }
            }
            else if ((gameObject as Item) != null)
            {
                Item item = (Item)gameObject;
                switch (linkFacingDirection)
                {
                    case Direction.Left:
                        if (item.position.X <= linkPosition.X)
                        {
                            Result = true;
                        }
                        break;
                    case Direction.Up:
                        if (item.position.Y >= linkPosition.Y)
                        {
                            Result = true;
                        }
                        break;
                    case Direction.Right:
                        if (item.position.X >= linkPosition.X)
                        {
                            Result = true;
                        }
                        break;
                    case Direction.Down:
                        if (item.position.Y <= linkPosition.Y)
                        {
                            Result = true;
                        }
                        break;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("FogOfWar couldn't find gameObject.");
            }

            return Result;
        }

        public void UpdateLink(Link link)
        {
            linkFacingDirection = link.currentDirection;
            linkPosition = link.Position;
        }

        public void ToggleFogOfWar()
        {
            fogOfWarToggle = !fogOfWarToggle;
        }
    }
}