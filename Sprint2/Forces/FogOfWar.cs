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
using MainGame.Forces;

namespace MainGame.Visibility
{
    //Used to handle Fog Of War (objects dissappear if not in view)
    public class FogOfWar : IForces
    {
        private static FogOfWar instance = new FogOfWar();

        private static bool fogOfWarToggle;
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
        }

        public Boolean FogOfWarCheck(IGameObject gameObject)
        {
            if (!fogOfWarToggle) return true;

            Boolean Result = false;

            //Uses direction of Link and compares position
            //to determine if the object is in the fog of war
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
                        if (item.pixelPosition.X <= linkPosition.X)
                        {
                            Result = true;
                        }
                        break;
                    case Direction.Up:
                        if (item.pixelPosition.Y <= linkPosition.Y)
                        {
                            Result = true;
                        }
                        break;
                    case Direction.Right:
                        if (item.pixelPosition.X >= linkPosition.X)
                        {
                            Result = true;
                        }
                        break;
                    case Direction.Down:
                        if (item.pixelPosition.Y >= linkPosition.Y)
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

            //Return true if the object should be visible
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

        public void ApplyForce(IGameObject gameObject)
        {
            //no-op
        }

        public void ToggleForce(IGameObject gameObject)
        {
            //no-op
        }
    }
}