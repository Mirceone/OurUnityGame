# My Souls Project

A third-person action RPG inspired by the Souls genre, built in Unity 6 with online multiplayer support via Unity Netcode for GameObjects.

## Overview

My Souls Project is a work-in-progress multiplayer souls-like game featuring character locomotion, networked player management, a title screen, and a persistent save system. The project uses a component-based character architecture designed to support both player-controlled and AI-controlled entities.

## Tech Stack

| Tool | Version |
|---|---|
| Unity | 6000.3.8f1 (Unity 6) |
| Render Pipeline | Universal Render Pipeline (URP) |
| Netcode | Unity Netcode for GameObjects 2.9.2 |
| Input | Unity Input System 1.18.0 |
| AI Navigation | Unity AI Navigation 2.0.10 |
| IDE | JetBrains Rider / Visual Studio |

## Project Structure

```
Assets/
├── Art/
│   ├── Animations/       # Humanoid animator clips
│   ├── Materials/        # Prototype materials
│   ├── Models/           # Character 3D models
│   ├── Textures/         # Prototype textures
│   └── titleScreen/      # Title screen UI art
├── Data/
│   ├── Animator Controllers/
│   └── Prefabs/          # Player, Camera, Input Manager prefabs
├── Scenes/
│   ├── Scene_Main_Menu_01.unity
│   └── Scene_World_01.unity
└── Scripts/
    ├── Character/        # Shared base character logic
    │   ├── CharacterManager.cs
    │   ├── CharacterLocomotionManager.cs
    │   ├── CharacterAnimatorManager.cs
    │   ├── CharacterNetworkManager.cs
    │   └── Player/       # Player-specific overrides
    │       ├── PlayerManager.cs
    │       ├── PlayerLocomotionManager.cs
    │       ├── PlayerInputManager.cs
    │       ├── PlayerCamera.cs
    │       ├── PlayerNetworkManager.cs
    │       └── PlayerUI/
    │           └── PlayerUIManager.cs
    ├── Menu Scene/
    │   └── TitleScreenManager.cs
    └── World Managers/
        └── WorldSaveGameManager.cs
```

## Getting Started

### Requirements

- Unity 6000.3.8f1 (Unity 6)
- Git LFS (if large assets are added later)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Mirceone/OurUnityGame.git
   ```
2. Open the project in **Unity Hub** using Unity version `6000.3.8f1`.
3. Let Unity import all packages (first open may take a few minutes).
4. Open `Assets/Scenes/Scene_Main_Menu_01.unity` to start from the title screen, or `Scene_World_01.unity` to jump into gameplay.

### Multiplayer Testing (Local)

This project uses **ParrelSync** to test multiplayer locally without building:

1. In Unity, go to **ParrelSync > Clones Manager**.
2. Create a clone of the project.
3. Open the clone in a second Unity Editor instance.
4. Press Play in both editors to test networked gameplay.

## Features (In Progress)

- [x] Character locomotion system (ground movement, rotation)
- [x] Networked player management via NGO
- [x] Player camera controller
- [x] New Input System integration
- [x] Title screen and main menu
- [x] World save game manager (foundation)
- [ ] Combat system
- [ ] Dodge / roll mechanic
- [ ] Enemy AI
- [ ] Save/load game data
- [ ] Multiplayer lobby

## Contributing

This is a private collaborative project. Push directly to `main` or open a branch for larger features.

Always make sure your local Unity project is closed before pulling to avoid file conflicts on `.asset` and `.prefab` files.
