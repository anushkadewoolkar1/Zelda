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

namespace MainGame.Forces
{

    public class Gravity : IForces
    {
        private static Gravity instance = new Gravity();

        Boolean gravityOn;

        public static Gravity Instance
        {
            get
            {
                return instance;
            }
        }

        private Gravity()
        {
            gravityOn = false;
        }

        private const float GRAVITY_FORCE = 4f; // Adjust as needed
        public void ApplyForce(IGameObject gameObject)
        {
            if (!gravityOn) return;

            Microsoft.Xna.Framework.Vector2 objectPosition;


            if ((gameObject as Enemy) != null)
            {
                objectPosition = ((Enemy)gameObject).position;
                objectPosition.Y += GRAVITY_FORCE;
                ((Enemy)gameObject).position = objectPosition;
                ((Enemy)gameObject).velocity = new Vector2(gameObject.Velocity.X, 1);
            } else if ((gameObject as Link) != null)
            {
                objectPosition = ((Link)gameObject).Position;
                objectPosition.Y += GRAVITY_FORCE;
                ((Link)gameObject).Position = objectPosition;
                ((Link)gameObject).velocity = new Vector2(gameObject.Velocity.X, 1);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Gravity: Could not identify game object.");
            }
        }

        public void ToggleForce(IGameObject gameObject)
        {
            gravityOn = !gravityOn;
        }
    }
}