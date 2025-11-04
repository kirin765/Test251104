# Top-Down Collection Mini Game

This Unity project implements a lightweight top-down collection game prototype. Move the player with the arrow keys or WASD to gather energy orbs before the timer expires. Each pickup increases your score and a new orb immediately spawns somewhere else in the arena.

## How to Play
1. Open `Assets/Scenes/SampleScene.unity` in the Unity Editor.
2. Press **Play**. The HUD instructs you to press the space bar to begin a round.
3. Collect as many orbs as possible before the timer reaches zero. The round ends automatically and you can restart with the space bar.

## Core Systems
- **PlayerController2D** – Rigidbody2D-driven movement with configurable speed.
- **CollectibleSpawner** – Keeps the arena populated with collectible prefabs and redraws gizmos for the spawn area when selected.
- **GameManager** – Handles the round loop, score/timer tracking, and HUD rendering via `OnGUI`.

The setup uses only built-in sprites and GUI rendering, so you can swap in your own art assets without additional dependencies.
