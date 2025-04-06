using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.Enums;

namespace MainGame.CollisionHandling
{
    public interface ICollisionHandler
    {
        void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide collisionSide);
    }
}
