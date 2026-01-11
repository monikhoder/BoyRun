# BoyRun - 2D Endless Runner

**BoyRun** is a 2D endless runner game developed as a capstone project for the **Unity Course at IT STEP Academy**. The player controls a character running through a procedurally generated world, collecting coins, avoiding enemies, and trying to beat their high score.

## 🎮 Game Description

In **BoyRun**, the goal is simple: run as far as you can! The game features an infinite level generator that spawns grounds, obstacles, and collectibles. As you collect coins, the game speed gradually increases, making it more challenging to survive.

## ✨ Key Features

* **Procedural Level Generation:** The game uses an object pooling system to infinitely spawn ground segments, ensuring the game never ends.
* **Dynamic Difficulty:** The player's movement speed increases by `0.1` for every **10 coins** collected, ramping up the challenge over time.
* **Enemy System:**
    * **Ground Enemies:** Obstacles on the ground that must be jumped over.
    * **Flying Enemies:** Enemies that spawn in the air, requiring careful timing to avoid.
* **Health & Damage:** The player starts with **5 lives**. Hitting an enemy reduces life and plays a damage sound effect. If life reaches 0, it's Game Over.
* **Score System:**
    * Coins increase your current score.
    * **High Score** is saved locally using `PlayerPrefs` and persists between game sessions.
* **Audio System:** Complete audio manager handling Background Music (BGM) and Sound Effects (SFX) for jumping, collecting items, and taking damage.

## 🕹️ Controls

* **Jump:** Press `Space` or `Left Mouse Click` to make the character jump.
* **Pause/Resume:** Press `Escape` to pause the game while playing.

## 🛠️ Technical Implementation

This project demonstrates various Unity and C# concepts learned during the course:
* **Singleton Pattern:** Used in `AudioManager` to ensure audio persists across scenes.
* **Object Pooling:** Implemented in `LevelGenerator` to efficiently manage memory by reusing ground objects instead of constantly destroying and instantiating them.
* **Script Communication:** Utilization of `GetComponent` and `FindFirstObjectByType` for communication between the Player, UI, and Game Managers.
* **Animation States:** Handling player animations for Running, Jumping, and Idle states.
* **Physics 2D:** Usage of Raycasts for ground detection and Rigidbody2D for movement.

## 🚀 How to Run the Project

1.  Clone this repository or download the source code.
2.  Open **Unity Hub**.
3.  Click **Add** and select the `BoyRun` folder.
4.  Open the project (Recommended Unity Version: 2021.3 or later).
5.  Open the scene located in `Assets/Scenes/GamePlay.unity`.
6.  Press the **Play** button to start.

## 📂 Project Structure

* `Assets/Script/`
    * `AudioManager.cs`: Handles BGM and SFX.
    * `LevelGenerator.cs`: Manages ground spawning and enemy/coin placement.
    * `PlayerController.cs`: Handles player physics, movement, and health.
    * `UIManager.cs`: Manages HUD, menus, and score tracking.
    * `ItemPickup.cs` & `hitEnemy.cs`: Logic for interactions with game objects.

## 👨‍💻 Credits

* **Developer:** Monikhoder
* **Institution:** IT STEP Academy Cambodia
* **Course:** Unity Game Development

---
*Created with ❤️ using Unity.*
