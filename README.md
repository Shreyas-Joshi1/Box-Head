# Last Stand: Top-Down Shooter

![alt text](<Screenshot 2026-01-09 010723.png>)

## Overview
This project is a **2D top-down shooter prototype** inspired by classic games like **Box Head** and **Restricted Zone**.  
It was developed with a focus on **core mechanics, clean architecture, and explainable logic**.

The goal of the prototype is to demonstrate:
- Player movement, aiming, and shooting
- Enemy AI with wave-based spawning
- Combat interactions and health systems
- A complete game loop (start -> play -> game over -> restart)

**Engine:** Unity (2D)  
**Perspective:** Top-Down 2D  
**Input:** Keyboard + Mouse

---

## Implemented Core Functionalities

### Player Controller
- Smooth **8-directional movement** using physics-based `Rigidbody2D`
- **Mouse-based aiming**, with the weapon always pointing toward the cursor
- **Projectile shooting** with left mouse click
- Fire rate limited shooting to prevent spamming

### Enemies (Zombies)
- Enemies spawn in **waves from screen edges**
- Each wave increases in difficulty by spawning more enemies
- Enemies continuously **move toward the player**
- Enemies are blocked by:
  - Obstacles
  - Level boundaries

### Combat & Interaction
- Player bullets **destroy enemies on impact**
- Enemies deal **contact-based damage** to the player with a cooldown
- Player has **finite health**
- Game ends when player health reaches zero
- **Solid obstacles** block:
  - Player movement
  - Enemy movement
  - Bullets

### Game Loop
- **Main Menu Scene**
  - Start Game
  - Quit Game
- **Game Scene**
  - Health bar
  - Wave counter
- **Game Over State**
  - Game pauses
  - Game Over UI displayed
- **Restart, Main Menu & Quit Game options** available after Game Over

<video controls src="Last Stand.mp4" title="Title"></video>

---

## Controls

| Action | Input |
|------|------|
| Move | **W / A / S / D** | **Up / Left / Down / Right** |
| Aim | **Mouse Cursor** |
| Shoot | **Left Mouse Button (LMB)** | **Enter** |
| UI Interaction | Mouse |

---

## Implementation Details

### Player Aiming & Shooting
- Mouse position is read in **screen space** and converted to **world space** using `Camera.ScreenToWorldPoint`
- Direction vector is calculated from the player to the mouse position
- Body/Weapon rotation is handled using `Mathf.Atan2` and applied only to a **child transform**, keeping visuals separate from logic
- Bullets are instantiated at a fire point and move using `Rigidbody2D` velocity in the aiming direction
- Shooting is rate-limited using a cooldown timer

---

### Enemy Spawning & Wave Logic
- Enemy spawning is managed by a dedicated `EnemyWaveSpawner`
- Each wave spawns: 
- enemies = baseEnemies + currentWave
- Enemies spawn:
- Just outside the camera view (screen edges)
- At a safe distance from the player
- Only if the position is not blocked by obstacles
- A **global enemy death event** is used to track when enemies die
- Once all enemies in a wave are defeated, the next wave starts automatically
- Wave number is displayed using a UI text element

---

### Damage Detection & Health System
- **Bullet -> Enemy**
- Handled via `OnCollisionEnter2D`
- Enemy is destroyed immediately on hit
- **Enemy -> Player**
- Contact-based damage using `OnCollisionStay2D`
- Damage cooldown prevents continuous damage every frame
- **Player Health**
- Managed via a centralized `PlayerHealth` component
- Health is displayed using a UI slider
- On reaching zero health:
  - Time scale is set to zero
  - Game Over UI is shown

---

## Project Structure
Assets/
 |-- Scripts/
 |   |-- PlayerMovement.cs
 |   |-- PlayerAim.cs
 |   |-- PlayerShoot.cs
 |   |-- PlayerInputHandler.cs
 |   |-- PlayerHealth.cs
 |   |-- Enemy.cs
 |   |-- EnemyWaveSpawner.cs
 |   |-- Bullet.cs
 |   |-- GameManager.cs
 |   `-- WaveNumberUI.cs
 |-- Prefabs/
 |   |-- 
 `-- Scenes/
     |-- MainMenu.unity
     `-- GameScene.unity

---

## UI & Scene Setup

### Main Menu Scene
- Game title text
- Start Game button
- Quit Game button

### Game Scene
- Player health bar (UI Slider)
- Wave number display
- Game Over panel:
  - Game Over text
  - Restart button
  - Main Menu button
  - Quit Game button
- Invisible boundary walls to restrict movement
- Obstacles placed throughout the level

![alt text](<Screenshot 2026-01-09 010731.png>)

---

## Enemy Navigation
- Enemy movement uses **Unity AI Navigation (NavMeshAgent) and NavMesh Plus (https://github.com/h8man/NavMeshPlus) in 2D**
- Walkable and unwalkable areas are defined using a baked NavMesh
- Obstacles are marked as non-walkable, enabling simple but effective pathfinding
- NavMeshAgent rotation and up-axis updates are disabled to support 2D movement

---

## Packages & Assets Used
- **Unity New Input System**
- **Unity AI Navigation (NavMesh) and NavMesh Plus (https://github.com/h8man/NavMeshPlus)**
- **Cinemachine**
- Free & licensed assets from **itch.io** (https://rgsdev.itch.io/free-cc0-modular-animated-vector-characters-2d)
- Prefab-based architecture for modularity

---

## Features Worth Mentioning
- Event-driven wave progression (decoupled enemy death tracking)
- Physics-based movement and combat
- Separation of visual and logic components
- Pause-safe gameplay logic using `Time.timeScale`
- Clean, modular scripts designed for easy explanation and extension

---