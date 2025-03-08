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


