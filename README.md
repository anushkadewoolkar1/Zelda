# Project Overview
This project is the second sprint of our game development series. The primary objectives for Sprint 2 include:

- Creating and refining interfaces for different aspects of the game (players, enemies, items, etc.).
- Implementing classes for each game object in our initial dungeon/level.
- Drawing, animating, and moving these objects as they will appear in the final game.
- Managing state changes for objects (e.g., directional facing, taking damage, etc.).
- Handling user input to control the player character 

# Team Members
- Aidan Roley
- Anushka Dewoolkar 
- Chlore Feller
- Kyle Dietrich
- Paul Paciorek
- Roy Volker Acapulco

# Controls
Below are the key mappings implemented for Sprint 2. 

# Player Controls
Arrow Keys or WASD: Move the player character (e.g., Link) and change facing direction.
Z or N: Perform a sword attack.
Num Key 1: Link uses an arrow
Num key 2: Link uses a boomerang 
Num key 3: Link ues a bomb
E: Cause the player to take damage (player enters damaged state).

Block/Obstacle Controls
T: Cycle to the previous block type in the block list.
Y: Cycle to the next block type in the block list.
(Blocks are currently stationary and non-interactive with other objects.)

Item Controls
U: Cycle to the previous item in the item list.
I: Cycle to the next item in the item list.
(Items move/animate as they would in the final game but do not interact with other objects yet.)

Enemy/NPC Controls
O: Cycle to the previous enemy/NPC in the list.
P: Cycle to the next enemy/NPC in the list.
(Enemies/NPCs move, animate, and may fire projectiles, but no collisions or damage checks are implemented yet.)

Other Controls
Q: Quit the game.
R: Reset the program to its initial state.

# Known Bugs and Issues
- Link's sword attacks do not go Link's facing direction
- Aquamentus and Goriyas doesn't throw projectiles
- Dodongo and Goriyas dont't face the directions based on the direction they are walking (they only face forward but they animate)

# Code Reviews
We performed code reviews focusing on:

Readability: Naming conventions, consistent formatting, and clear comments.
Maintainability: Reducing coupling, following design patterns, and preparing for future changes.
Reviews are documented in our \CodeReviews folder.