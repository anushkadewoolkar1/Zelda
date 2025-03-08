# Project Overview
This project is Sprint 3 of our game development series. 
The primary objectives for Sprint 3 that were implemented were:

Interface and Class Development:
We Created and refined interfaces for various aspects of the game (players, enemies, items, projectiles, blocks, etc.)
and implemented classes for each game object in our initial dungeon/level.

Rendering and Animation:
Implemented drawing, animating, and movement logic for game objects 
so that they visually behave as expected in the final game.

State Management:
Managed state changes for objects, including directional facing, taking damage, 
and triggering invulnerability. 

Collision Handling:
Developing a comprehensive collision system that:

Uses bounding boxes (via an IGameObject interface) for collision detection.
Implements a sort-and-sweep algorithm to optimize collision checks between dynamic objects.
Separates collision detection from collision response by dispatching events to dedicated 
collision handler classes  Uses velocity vectors to help determine collision sides for 
proper response logic.

# Team Members
- Aidan Roley
- Anushka Dewoolkar 
- Chloe Feller
- Kyle Dietrich
- Paul Paciorek
- Roy Volker Acapulco

# Controls
Below are the key mappings implemented for Sprint 3. 

# Player Controls
Arrow Keys or WASD: Move the player character (e.g., Link) and change facing direction.
Z or N: Perform a sword attack.
Num Key 1: Link uses an arrow
Num key 2: Link uses a boomerang 
Num key 3: Link ues a bomb
Num key 4: Link uses sword beam
E: Cause the player to take damage 

Other Controls
Left Click: Moves to next room
Q: Quit the game.
R: Reset the program to its initial state.

# Known Bugs and Issues
- The water still currently acts as a normal collidable block
  (it will eventually be impassable by link and enemies but not by projectiles)
- Room 0,0 is a 2d section that still acts 3d and the enemies dont move around

# Code Reviews
We performed code reviews focusing on:

Readability: Naming conventions, consistent formatting, and clear comments.
Maintainability: Reducing coupling, following design patterns, and preparing for future changes.
Reviews are documented in our \CodeReviews folder.