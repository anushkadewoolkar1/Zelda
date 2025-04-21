using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MainGame.CollisionHandling;
using MainGame.States;
using MainGame.Sprites;
using Microsoft.Xna.Framework;
using Zelda.Enums;

namespace MainGame.Forces
{
    public interface IForces
    {
        public void ApplyForce(IGameObject gameObject);
        
        public void ToggleForce(IGameObject gameObject);
    }
}