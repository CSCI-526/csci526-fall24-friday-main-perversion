# 🌀 Perversion: A Strategic 3D Platformer

**Perversion** is a captivating and strategic 3D platformer that challenges players to rotate **gravity** and **perspective** to uncover hidden paths, overcome obstacles, and reach their goals. This game is built with Unity and showcases experimental gameplay built on unique camera and gravity manipulation mechanics.

🎮 **Play the Game Here**: [https://distr1ct9.github.io/PerversionGold/](https://distr1ct9.github.io/PerversionGold/)

📄 **Game Design Document (GDD)**: [https://docs.google.com/document/d/1Q3tvTHk8SgLvbEjZhkDaI895mzI5wPUF](https://docs.google.com/document/d/1Q3tvTHk8SgLvbEjZhkDaI895mzI5wPUF)


---

## 🎮 Game Overview

- **Genre**: Puzzle-Platformer / Experimental 3D Platformer  
- **Perspective**: Third-person  
- **Core Concept**: Rotate gravity and change perspective (X ↔ Z view) to navigate a spatially complex world  
- **Player Goal**: Use smart movement, gravity shifting, and camera perspective swapping to reach the end point of each level

---

## 🧠 Core Mechanics

| Feature | Description |
|--------|-------------|
| **Movement** | Move left/right using keyboard controls (A/D keys) |
| **Gravity Rotation** | Press `G` to rotate gravity, allowing traversal across walls and ceilings |
| **Perspective Change** | Press `H` to switch between X and Z axis viewpoints to reveal different platform layouts |
| **Respawn System** | Fall-off detection automatically returns the player to the most recent checkpoint |
| **Progress Evaluation** | A success check determines if the player has reached the goal area |

---

## 🕹️ Controls

| Action | Key |
|--------|-----|
| Move Left / Right | `A`, `D` |
| Rotate Gravity | `G` |
| Switch Perspective | `H` |

---

## 📽️ Features

- Dual-view exploration system: Platforms become accessible only when viewed from the correct perspective
- Smooth animated gravity rotation and camera transitions to support player orientation
- Strategic level design that encourages spatial reasoning and experimentation
- Fully implemented checkpoint and respawn mechanics

---

## 📁 Project Structure (Unity)

- `PlayerController.cs`: Handles movement and physics-based gravity switching
- `CameraManager.cs`: Manages X/Z view transitions and animations
- `CheckpointSystem.cs`: Stores respawn points and handles player reset
- `GameManager.cs`: Oversees game logic, win condition checks, and perspective tracking

---

## 🧪 Future Work / Extensions

- Add time-based challenge modes or collectibles to deepen gameplay
- Integrate player performance telemetry (e.g., fall count, time per level)
- Extend the perspective mechanic to support additional axes (Y or diagonal view transitions)
- Expand level variety and add multi-stage puzzles

---

## 👨‍💻 Developers

- Jianbang Sun (Lead Developer, Unity Engineer)
- Sai Vibhav Chirravuri (Product Manager)
- Hongbin Dong (Level Designr, Unity Engineer)
- Vasudha Padala (Stenographer, UI/UX Designer)
- Yuchen yang (Data Analyzer, Stenographer)
- Zhuoning Wu (Game Tester)

---

## 🛠️ Tech Stack

- **Engine**: Unity
- **Language**: C# 
- **Version Control**: Git/GitHub

---



