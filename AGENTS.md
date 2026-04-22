# AGENTS.md

## Project Overview

This repository contains a Unity-based 2D pixel-style narrative / investigation / puzzle demo project.

The project is currently in the framework-building and feature-integration stage.  
Its current focus is on establishing stable and extensible core systems first, then gradually expanding scene content, interaction logic, story flow, and gameplay depth.

Core implemented or partially implemented areas currently include:

- Main menu
- Pause menu
- Scene loading
- Basic audio system
- Basic dialogue system
- Basic task system
- Basic save/load system
- Investigation interaction triggers

---

## General Working Principles

When modifying this repository, always prefer:

- **minimal changes**
- **clear responsibility boundaries**
- **low-risk refactors**
- **preserving existing Unity Inspector bindings whenever possible**

Do not perform broad restructuring unless explicitly requested.

If a task can be solved with a smaller targeted change, prefer the smaller change.

---

## Repository Structure Guidelines

### Main Script Directories

- `Assets/Scripts/Core`
  - Shared base types, common data structures, enums, and general-purpose foundational code

- `Assets/Scripts/Deduction`
  - Deduction / reasoning related gameplay logic

- `Assets/Scripts/Dialogue`
  - Dialogue data types and dialogue-specific logic

- `Assets/Scripts/Evidence`
  - Evidence / clue related systems

- `Assets/Scripts/Interaction`
  - Generic interaction logic

- `Assets/Scripts/Investigation`
  - Investigation scene logic and scene-specific interaction flow

- `Assets/Scripts/Managers`
  - Global managers and high-level system control

- `Assets/Scripts/Player`
  - Player-specific runtime logic such as save/load restoration

- `Assets/Scripts/Task`
  - Task / quest system logic

- `Assets/Scripts/UI`
  - UI controllers, menu logic, panels, and button-related behavior

- `Assets/Scripts/Utils`
  - Utility helpers and reusable support code

---

## Architecture Rules

### Managers
Files under `Assets/Scripts/Managers` should handle:

- global state
- cross-system coordination
- game-wide control flow
- persistent systems when applicable

Examples:
- `AudioManager`
- `DialogueManager`
- `GameManager`
- `SaveManager`
- `TaskManager`

Managers should not take on too much direct UI presentation logic.

---

### UI Controllers
Files under `Assets/Scripts/UI` should handle:

- panel visibility
- button responses
- text refresh
- UI data presentation
- binding UI elements to system-level managers

Examples:
- `MainMenuController`
- `PauseMenuController`
- `SaveSlotUI`
- `TaskUIController`
- `AudioController`
- `AudioSettingsBinder`
- `UIButtonSound`

UI scripts should generally call into managers rather than duplicating manager logic.

---

### Data Classes
Data classes should mainly store data and avoid large amounts of procedural control logic.

Examples:
- `GameTaskData`
- `DialogueLine`
- `SaveData`

---

### Trigger / Loader Classes
Trigger-style classes should mainly respond to interaction entry points, collisions, or player actions.

Loader-style classes should mainly apply saved data back onto runtime objects.

Examples:
- `InteractionDialogueTrigger`
- `PlayerSaveLoader`

---

## Important Existing Script Intent

The following responsibilities should be preserved unless explicitly changed:

### Core Data
- `GameTaskData`: task-related data structure
- `DialogueLine`: dialogue line data structure
- `SaveData`: save data structure

### Global Managers
- `AudioManager`: global BGM / SFX management
- `DialogueManager`: dialogue flow control
- `GameManager`: overall game flow coordination
- `SaveManager`: save/load management
- `TaskManager`: task state management
- `PauseManagerrScript`: pause state control  
  - Note: naming can be improved later, but avoid renaming unless requested because it may affect Unity bindings

### UI and Flow
- `MainMenuController`: main menu UI control
- `PauseMenuController`: pause menu UI control
- `SaveSlotUI`: save slot UI
- `TaskUIController`: task UI
- `AudioController`: audio settings UI control
- `AudioSettingsBinder`: binds UI settings to audio systems
- `SceneLoader`: scene loading entry point
- `MainMenuGameEntry`: game entry flow from main menu
- `UIButtonSound`: UI sound feedback

### Runtime / Interaction
- `InteractionDialogueTrigger`: interaction-triggered dialogue entry
- `PlayerSaveLoader`: restore player state/position after loading

---

## Editing Rules

When editing code in this repository:

1. Do not rename serialized fields unless necessary.
2. Do not remove public methods that are likely referenced by Unity Buttons or Inspector events unless explicitly requested.
3. Avoid changing scene object names unless required.
4. Avoid changing prefab structure unless the task specifically requires it.
5. Do not rewrite entire systems when a local fix is enough.
6. If a bug can be fixed with a targeted patch, do not perform a large refactor.
7. Prefer compatibility with existing Inspector references.

---

## Unity-Specific Safety Rules

Because this is a Unity project, code changes may affect Inspector bindings, scene references, and prefab setups.

When making changes, always consider:

- whether a `public` field or `[SerializeField]` field is used in Inspector
- whether a method is linked to a Button `OnClick`
- whether a script is expected to be attached to a specific GameObject
- whether a scene transition relies on a specific object existing
- whether a prefab reference may break if a type or field changes

If a change may require Unity-side manual action, clearly state that in the final summary.

---

## Preferred Coding Style

### General
- Keep code readable and simple.
- Favor small methods with clear purposes.
- Avoid unnecessary abstraction.
- Match the existing project style where practical.

### Naming
Prefer these suffix conventions where possible:

- `Manager` for system managers
- `Controller` for UI and flow controllers
- `Data` for pure data structures
- `Loader` for restore/load helpers
- `Trigger` for interaction entry scripts

Do not rename existing files just to satisfy naming rules unless explicitly requested.

---

## Preferred Change Scope

### Good tasks for modification
- Fixing a specific bug in one subsystem
- Adding a small focused feature
- Improving local logic in menu / save / task / dialogue modules
- Clarifying code with comments when requested
- Creating or updating project documentation

### Avoid unless explicitly asked
- large architectural rewrites
- massive folder restructuring
- changing many script names at once
- broad scene/prefab reorganization
- changing multiple unrelated systems in one pass

---

## Manual Verification Checklist

After code changes, especially in Unity-related systems, mention whether the user should manually verify:

- Inspector references
- Button `OnClick` bindings
- panel active states
- scene loading targets
- prefab references
- save/load runtime behavior
- player position restoration
- pause / resume behavior
- audio slider functionality
- task UI refresh behavior

---

## Common Risk Areas in This Project

Pay extra attention to the following areas:

### Save / Load Flow
Potentially affected files:
- `SaveManager`
- `SaveData`
- `SaveSlotUI`
- `PlayerSaveLoader`
- `SceneLoader`

Typical risks:
- save data not matching runtime state
- player position not restored correctly
- Continue flow entering the scene but not restoring data

---

### Pause / Menu / Audio Interaction
Potentially affected files:
- `PauseManagerrScript`
- `PauseMenuController`
- `AudioManager`
- `AudioController`
- `AudioSettingsBinder`

Typical risks:
- UI panel switching inconsistencies
- pause state not matching `Time.timeScale`
- audio not pausing/resuming correctly
- settings UI not syncing with stored values

---

### Main Menu Entry Flow
Potentially affected files:
- `MainMenuController`
- `MainMenuGameEntry`
- `SceneLoader`
- `SaveManager`

Typical risks:
- New Game and Continue sharing incorrect logic
- entering scene without correct initialization
- inconsistent save selection behavior

---

## Documentation Rules

If documentation is updated:

- keep README professional and clear
- use markdown headings consistently
- prefer concise, structured explanation
- keep project-specific terminology consistent

---

## Final Response Expectations for Code Changes

When completing a code task, always summarize:

1. which files were changed
2. why they were changed
3. whether Unity Inspector rebinding may be needed
4. whether scene or prefab verification is recommended
5. any remaining risks or follow-up steps

---

## Notes

This project is actively evolving.  
Preserve stability where possible, and prioritize helping the user maintain a workable and understandable Unity project structure.
