Collision System Implementation

Two key parts: collision detection and collision response.

### Collision Detection
	Bounding Boxes & Rectangles:
	Each game object (Link, enemies, items, blocks, etc.) NEEDS to implement an IGameObject interface, 
	which requires a BoundingBox (a Rectangle) and a Velocity vector. We use the Rectangle.Intersects 
	and Rectangle.Intersect methods to check if two objects overlap.

- Sort and Sweep Algorithm:
	CollisionManager sorts all dynamic objects (those that move, such as Link, enemies, and projectiles)
	by the x-coordinate of their bounding boxes. Then, by “sweeping” through the sorted list, 
	we only check collisions between objects that are close enough on the x-axis.

- Determining the Collision Side:
	Once two objects are found to be intersecting,
	we determine the side of the collision (left, right, top, or bottom)
	by analyzing the intersection dimensions and the objects’ velocity vectors. 
	For instance, if the intersection width is smaller than its height and the object is moving right, 
	we assume the collision is on its right side.

### Collision Response

	I’ve created an interface ICollisionHandler along with several specific handler classes

	- PlayerBlockCollisionHandler: Handles collisions between Link and blocks.

	- PlayerItemCollisionHandler: Handles when Link collides with an item, triggering the item pickup logic.

	- EnemyBlockCollisionHandler: Adjusts enemy behavior when they hit static objects 

	- PlayerEnemyProjectileCollisionHandler: Applies damage to Link when he collides with enemy projectiles.

### AllCollisionManager:
	This class acts as a central dispatcher. When a collision is detected, the AllCollisionManager 
	examines the types of the colliding objects and calls the appropriate collision handler. 

### TODO: 
- Implement IGameObject interface for each game object (enemy i think is the only one not implemented, 
														also probably the level to so link cant walk out of the map)

- Complete Response Logic for each collision handler

- We need to integrate the collision detection and response into the game’s main update loop. 

- We might need to add additional collision handlers (Link vs. enemy collisions, enemy vs. enemy)

