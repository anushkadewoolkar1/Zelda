using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.Enums;

namespace MainGame.CollisionHandling
{
    public interface ICollision
    {
        // Two objects involved in collion
        IGameObject ObjectA { get; }
        IGameObject ObjectB { get; }

        // Side of collision
        CollisionSide Side { get; }
    }
}
