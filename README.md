# Project Overview
This project is Sprint 4 of our game development series. 
The primary objectives for Sprint 4 that were implemented were:

Interface and Class Development:
We Created and refined interfaces for various aspects of the game (players, enemies, items, projectiles, blocks, inventory,  etc.)
and implemented classes for each game object in our initial dungeon/level.

Rendering and Animation:
Implemented drawing, animating, and movement logic for game objects 
so that they visually behave as expected in the final game.

State Management:
Managed state changes for objects, including directional facing, taking damage, 
and triggering invulnerability. 

Collision Handling Fixing:
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
Z: Perform a sword attack.
N: Use Item
Tab: Cycle Item (you must hit tab after picking up an item to select it)

Other Controls
Left Click: Moves to next room
Q: Quit the game.
R: Reset the program to its initial state.
F: Toggle program in and out of full-screen
K: Open inventory menu
L: Open settings menu
+: Increase program volume
-: Decrease program volume



# Known Bugs and Issues
- Goriyas despawn upon throwing boomerang


# Code Reviews
We performed code reviews focusing on:

Readability: Naming conventions, consistent formatting, and clear comments.
Maintainability: Reducing coupling, following design patterns, and preparing for future changes.
Reviews are documented in our \CodeReviews folder.
