# Top-Down Collection Mini Game

This Unity project implements a lightweight top-down collection game prototype. Move the player with the arrow keys or WASD to gather energy orbs before the timer expires. Each pickup increases your score and a new orb immediately spawns somewhere else in the arena.

## How to Play
1. Open `Assets/Scenes/SampleScene.unity` in the Unity Editor.
2. Press **Play**. The HUD instructs you to press the **Space** bar (or the **Start / South** button on a gamepad) to begin a round.
3. Collect as many orbs as possible before the timer reaches zero. The round ends automatically and you can restart with the same input.

## Core Systems
- **PlayerController2D** – Rigidbody2D-driven movement that now supports both the legacy and new Input Systems (keyboard/gamepad).
- **PlayerMovementAnimator** – Adds a light bob-and-sway motion plus sprite flipping so the miner feels active while moving.
- **CollectibleSpawner** – Keeps the arena populated with collectible prefabs and redraws gizmos for the spawn area when selected.
- **GameManager** – Handles the round loop, score/timer tracking, and HUD rendering via `OnGUI`, accepting both legacy and new input events for starting rounds.

The setup uses only built-in sprites and GUI rendering, so you can swap in your own art assets without additional dependencies.

## Customizing the Miner Visuals
- The playable character keeps its physics components on the `Player` root object, while the sprite now lives on the child `Player/Player Sprite` transform that the movement animator targets.
- Replace the sprite on that child (or add your own Animator) to bring in custom miner artwork without disturbing collisions or motion scripting.
