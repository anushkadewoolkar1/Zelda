using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.Enums;

namespace Sprint0.CollisionHandling
{
    public interface ICollision
    {
        // Two objects invloced in collion
        // IGameObject ObjectA { get; }
        // IGameObject ObjectB { get; }

        // Side of collision
        CollisionSide Side { get; }
    }
}
