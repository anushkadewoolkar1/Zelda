using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sprint0.Collision;
using Sprint0.CollisionHandling;
using Zelda.Enums;
using Sprint0.States;
using Sprint0.ILevel;
using System.ComponentModel.Design;


public class CollisionManager
{
    private AllCollisionManager allCollisionManager;

    public CollisionManager()
    {
        allCollisionManager = new AllCollisionManager();
    }

    /// Checks for collisions among dynamic objects using a sort-and-sweep algorithm.
    public void CheckDynamicCollisions(List<IGameObject> dynamicObjects, Level currLevel)
    {

        // Sort dynamic objects by the left side (X coordinate) of their bounding boxes.
        dynamicObjects.Sort((a, b) => a.BoundingBox.X.CompareTo(b.BoundingBox.X));

        // Loop through the sorted objects.
        for (int i = 0; i < dynamicObjects.Count; i++)
        {

            IGameObject objA = dynamicObjects[i];

            Rectangle levelIntersection = Rectangle.Intersect(objA.BoundingBox, currLevel.BoundingBox);

            if ((levelIntersection.Width * levelIntersection.Height) < (objA.BoundingBox.Width * objA.BoundingBox.Height))
            {
                DispatchLevelCollisions(objA, currLevel, levelIntersection);
            }

            // Compare with subsequent objects.
            for (int j = i + 1; j < dynamicObjects.Count; j++)
            {
                IGameObject objB = dynamicObjects[j];

                // If objB's left side is beyond objA's right side, no more objects can overlap on the X-axis.
                if (objB.BoundingBox.X > objA.BoundingBox.Right)
                    break;

                // Check if objA and objB actually intersect.
                if (objA.BoundingBox.Intersects(objB.BoundingBox))
                {
                    CollisionSide side;
                    // Determine collision side. We use the objects' velocities and the intersection rectangle.
                    if (objA is Block)
                    {
                        side = DetermineCollisionSide(objB, objA);
                    }
                    else
                    {
                        side = DetermineCollisionSide(objA, objB);
                    }

                    // Here, we call appropriate collision response handler.
                    if (objA is Item || objB is Item)
                    {
                        PlayerItemCollisionHandler itemCollisionHandler = new PlayerItemCollisionHandler();
                        itemCollisionHandler.HandleCollision(objA, objB, side);
                    }

                    if (objA is Block ||  objB is Block)
                    {
                        if (objA is Link || objB is Link)
                        {
                            LinkBlockCollisionHandler blockCollisionHandler = new LinkBlockCollisionHandler();
                            if (objA is Block)
                            {
                                blockCollisionHandler.HandleCollision(objB, objA, side);
                            }
                            else
                            {
                                blockCollisionHandler.HandleCollision(objA, objB, side);
                            }
                        } else if (objA is Enemy || objB is Enemy)
                        {
                            EnemyBlockCollisionHandler blockCollisionHandler = new EnemyBlockCollisionHandler();
                            if (objA is Enemy) {
                                blockCollisionHandler.HandleCollision(objA, objB, side);
                            } else
                            {
                                blockCollisionHandler.HandleCollision(objB, objA, side);
                            }
                        }
                    }

                    if (objA is Enemy || objB is Enemy)
                    {
                        LinkEnemyCollisionHandler enemyCollisionHandler = new LinkEnemyCollisionHandler();
                        if (objA is Link)
                        {
                            enemyCollisionHandler.HandleCollision(objA, objB, side);
                        } else
                        {
                            enemyCollisionHandler.HandleCollision(objB, objA, side);
                        }
                    }

                    // For now, debug message.
                    Console.WriteLine($"Collision detected between {objA} and {objB} on side: {side}");

                }
            }
        }
    }

    private void DispatchLevelCollisions(IGameObject objA, Level currLevel, Rectangle intersection)
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
            collisionHandler = new BlockLevelCollisionHandler();
        }
        CollisionSide side;
        int heightDiff = (objA.BoundingBox.Height - intersection.Height), widthDiff = (objA.BoundingBox.Width - intersection.Width);
        if (heightDiff > widthDiff)
        {
            if (objA.Velocity.Y > 0)
            {
                side = CollisionSide.Bottom;
            } else
            {
                side = CollisionSide.Top;
            } 
        } else
        {
            if (objA.Velocity.X > 0)
            {
                side = CollisionSide.Left;  
            } else
            {
                side = CollisionSide.Right;
            }
        }
        collisionHandler.HandleCollision(objA, currLevel, side);
    }

    /// Determines the collision side for objA relative to objB using their velocities and intersection.
    private CollisionSide DetermineCollisionSide(IGameObject objA, IGameObject objB)
    {
        Rectangle intersection = Rectangle.Intersect(objA.BoundingBox, objB.BoundingBox);

        // If the intersection width is smaller than its height, assume a horizontal collision.
        if (intersection.Width < intersection.Height)
        {
            // Use the velocity of objA to decide: if moving right, collision is on the right; else, left.
            if (objA.Velocity.X < 0)
                return CollisionSide.Right;
            else
                return CollisionSide.Left;
        }
        else
        {
            // Vertical collision: if moving down, collision is at the bottom; else, top.
            if (objA.Velocity.Y < 0)
                return CollisionSide.Bottom;
            else
                return CollisionSide.Top;
        }
    }
}

