using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MainGame.Collision;
using MainGame.CollisionHandling;
using Zelda.Enums;
using MainGame.States;
using MainGame.Display;
using System.ComponentModel.Design;
using MainGame.Sprites;
using Microsoft.Xna.Framework.Graphics;

public class CollisionManager
    
{
    public int debug = 0;
    public int levelDebug = 0;
    CollisionSide lastSide = CollisionSide.Left;

    public void CheckDynamicCollisions(List<IGameObject> dynamicObjects, LevelManager currLevel)
    {
        SortDynamicObjects(dynamicObjects);

        CheckLevelCollisions(dynamicObjects, currLevel);

        CheckObjectCollisions(dynamicObjects);
    }

    // Sorts the dynamic objects by the left edge of their bounding boxes
    private void SortDynamicObjects(List<IGameObject> dynamicObjects)
    {
        dynamicObjects.Sort((a, b) => a.BoundingBox.X.CompareTo(b.BoundingBox.X));
    }

    // Checks and dispatches collisions between each dynamic object and the level boundary
    private void CheckLevelCollisions(List<IGameObject> dynamicObjects, LevelManager currlevel)
    {
        foreach (var objA in dynamicObjects)
        {
            Rectangle levelIntersection = Rectangle.Intersect(objA.BoundingBox, currlevel.BoundingBox);

            // If partial or no overlap, then collision
            bool isCollidingWithLevel = (levelIntersection.Width * levelIntersection.Height)
                                        < (objA.BoundingBox.Width * objA.BoundingBox.Height);

            if (isCollidingWithLevel)
            {
                levelDebug++;
                DispatchLevelCollisions(objA, currlevel, levelIntersection);
            }

        }
    }

    // Checks and dispatches collisions among all dynamic objects against one another
    private void CheckObjectCollisions(List<IGameObject> dynamicObjects)
    {
        for (int i = 0; i < dynamicObjects.Count; i++)
        {
            IGameObject objA = dynamicObjects[i];

            // compare with objects
            for (int j = i + 1; j < dynamicObjects.Count; j++)
            {
                IGameObject objB = dynamicObjects[j];

                // If objB's left side is beyond objA's right side,
                // there's no more possible overlap on the X-axis, so break
                if (objB.BoundingBox.X > objA.BoundingBox.Right)
                    break;

                // Check if objA and objB actually intersect
                if (objA.BoundingBox.Intersects(objB.BoundingBox))
                {
                    ProcessCollisionPair(objA, objB);
                }
            }
        }
    }

    // Determines the proper collision handler calls for a single colliding pair of game objects
    private void ProcessCollisionPair(IGameObject objA, IGameObject objB)
    {
        // If both blocks skip
        if (objA is Block && objB is Block)
        {
            return;
        }

        // Determine side
        CollisionSide side = DetermineCollisionSide(objA, objB);

        // Link -> EnemyProjectile
        if ((objA is Link && objB is EnemyProjectileSprite) ||
            (objA is EnemyProjectileSprite && objB is Link))
        {
            var projectileHandler = new LinkEnemyProjectileCollisionHandler();
            projectileHandler.HandleCollision(objA, objB, side);
            return;
        }

        // Link -> Item
        if (objA is Item || objB is Item)
        {
            var itemCollisionHandler = new LinkItemCollisionHandler();
            itemCollisionHandler.HandleCollision(objA, objB, side);
        }

        // Link -> Block or Enemy -> Block
        if (objA is Block || objB is Block)
        {
            if (objA is Block)
            {
                side = DetermineCollisionSide(objB, objA);
            }
            else
            {
                side = DetermineCollisionSide(objA, objB);
            }

            if (objA is Link || objB is Link)
            {
                var LinkBlockHandler = new LinkBlockCollisionHandler();
                if (objA is Block) LinkBlockHandler.HandleCollision(objB, objA, side);
                else LinkBlockHandler.HandleCollision(objA, objB, side);
            }
            else if (objA is Enemy || objB is Enemy)
            {
                var enemyBlockHandler = new EnemyBlockCollisionHandler();
                if (objA is Enemy) enemyBlockHandler.HandleCollision(objA, objB, side);
                else enemyBlockHandler.HandleCollision(objB, objA, side);
            }
        }

        // Link -> Enemy
        if (objA is Enemy || objB is Enemy)
        {
            if ((objA is Link && objB is Enemy) || (objB is Link && objA is Enemy))
            {
                var linkEnemyHandler = new LinkEnemyCollisionHandler();
                if (objA is Link) linkEnemyHandler.HandleCollision(objA, objB, side);
                else linkEnemyHandler.HandleCollision(objB, objA, side);
            }
        }

        // Enemy -> LinkProjectile
        if ((objA is Enemy && IsLinkProjectile(objB)) ||
            (objB is Enemy && IsLinkProjectile(objA)))
        {
            var projectileHandler = new EnemyLinkProjectileCollisionHandler();
            if (objA is Enemy) projectileHandler.HandleCollision(objA, objB, side);
            else projectileHandler.HandleCollision(objB, objA, side);
        }

        if ((objA is Enemy && objB is HitBox) ||
            (objB is Enemy && objA is HitBox))
        {
            var projectileHandler = new EnemyLinkAttackCollisionHandler();
            if (objA is Enemy) projectileHandler.HandleCollision(objA, objB, side);
            else projectileHandler.HandleCollision(objB, objA, side);
        }
    }

    // Dispatches collisions involving a single object and the level
    private void DispatchLevelCollisions(IGameObject objA, LevelManager currLevel, Rectangle intersection)
    {
        ICollisionHandler collisionHandler = null;

        if (objA is Link)
        {
            collisionHandler = new LinkLevelCollisionHandler();
        }
        else if (objA is Enemy)
        {
            collisionHandler = new EnemyLevelCollisionHandler();
        }
        else if (objA is Block)
        {
            objA.Destroy();
            return;
        } 
        else if (objA is Item)
        {
            collisionHandler = new LinkItemCollisionHandler();
        } 
        else if (objA is ProjectileSprite)
        {
            return;
        }

        // Figure out which side
        CollisionSide side;
        int heightDiff = (objA.BoundingBox.Height - intersection.Height);
        int widthDiff = (objA.BoundingBox.Width - intersection.Width);

        if (heightDiff > widthDiff)
        {
            if (objA.Velocity.Y > 0) side = CollisionSide.Bottom;
            else side = CollisionSide.Top;
        } 
        else
        {
            if (objA.Velocity.X < 0) side = CollisionSide.Left;
            else side = CollisionSide.Right;
        }

        if (collisionHandler != null)
        {
            collisionHandler.HandleCollision(objA, currLevel, side);
        }
        
    }
    private CollisionSide DetermineCollisionSide(IGameObject objA, IGameObject objB)
    {
        debug++;

        Rectangle intersection = Rectangle.Intersect(objA.BoundingBox, objB.BoundingBox);
        CollisionSide side;

        if (intersection.Width < intersection.Height)
        {
            if (objA.Velocity.X != 0)
            {
                side = objA.Velocity.X < 0 ? CollisionSide.Right : CollisionSide.Left;
            }
            else
            {
                side = objB.Velocity.X < 0 ? CollisionSide.Right : CollisionSide.Left;
            }
        }
        else
        {
            if (objA.Velocity.Y != 0)
            {
                side = objA.Velocity.Y <= 0 ? CollisionSide.Bottom : CollisionSide.Top;
            }
            else if (objB.Velocity.Y != 0)
            {
                side = objB.Velocity.Y <= 0 ? CollisionSide.Bottom : CollisionSide.Top;
            }
            else
            {
                if (objA.Velocity.X != 0)
                {
                    side = objA.Velocity.X < 0 ? CollisionSide.Right : CollisionSide.Left;
                }
                else
                {
                    side = objB.Velocity.X < 0 ? CollisionSide.Right : CollisionSide.Left;
                }
            }
        }

        lastSide = side;
        return side;
    }
/*
    /// Determines the collision side for objA relative to objB using their velocities and intersection.
    private CollisionSide DetermineCollisionSide(IGameObject objA, IGameObject objB)
    {


        debug++;
        Rectangle intersection = Rectangle.Intersect(objA.BoundingBox, objB.BoundingBox);

        // If the intersection width is smaller than its height, assume a horizontal collision.
        if (intersection.Width < intersection.Height)
        {

            if (objA.Velocity.X != 0)
            {
                // Use the velocity of objA to decide: if moving right, collision is on the right; else, left.
                if (objA.Velocity.X < 0)
                    return CollisionSide.Right;
                else
                    return CollisionSide.Left;
            }
            else
            {
                if (objB.Velocity.X < 0)
                    return CollisionSide.Right;
                else
                    return CollisionSide.Left;
            }
        }
            
        else
        {

            if (objA.Velocity.Y != 0)
            {
                // Vertical collision: if moving down, collision is at the bottom; else, top.
                if (objA.Velocity.Y <= 0)
                    return CollisionSide.Bottom;
                else
                    return CollisionSide.Top;
            }
            else
            {
                if (objB.Velocity.Y <= 0)
                    return CollisionSide.Bottom;
                else if (objB.Velocity.Y != 0)
                    return CollisionSide.Top;

                else
                {
                    if (objA.Velocity.X != 0)
                    {
                        // Use the velocity of objA to decide: if moving right, collision is on the right; else, left.
                        if (objA.Velocity.X < 0)
                            return CollisionSide.Right;
                        else
                            return CollisionSide.Left;
                    }
                    else
                    {
                        if (objB.Velocity.X < 0)
                            return CollisionSide.Right;
                        else
                            return CollisionSide.Left;
                    }
                }
            }
            
        }
    }
*/

    // Helper method for determining if object is Link's projectile 
    private bool IsLinkProjectile(IGameObject obj)
    {
        // If Link's projectiles are of this type
        return obj is ProjectileSprite;
    }
}

