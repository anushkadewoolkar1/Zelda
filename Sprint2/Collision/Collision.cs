using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.Enums;

namespace MainGame.CollisionHandling
{
    public class Collision : ICollision
    {
        public IGameObject ObjectA { get; private set; }
        public IGameObject ObjectB { get; private set; }
        public CollisionSide Side { get; private set; }

        public Collision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            ObjectA = objA;
            ObjectB = objB;
            Side = side;
        }
    }
}
