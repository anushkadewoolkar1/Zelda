using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Sprint0.CollisionHandling
{
    public interface IGameObject
    {
        Rectangle BoundingBox { get; }
        Vector2 Velocity { get; }

    }
}
