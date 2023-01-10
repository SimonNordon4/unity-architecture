# Classic Architecture Progression

Create an ever increasing complex micro game, by refactoring using different architecture patterns.

## Monoscript

Create a simple prototype game in a single script. Move a player, spawn enemies, shoot player.

## Gameobject-Components

Expand the game using Gameobject-Components architecture to include pickups.

## Singletons

Improve the game by using Singletons.

## Interfaces

Improve reusability by using interfaces.

## Unity-Events

Instead of polling for values changes, use Observable pattern and unity events.

## Coroutines / Await

Introduce using Coroutines 

## State 

Introduce State Pattern and the state machine.

## Commands

Introduce the use of commands instead of Unity-Events.

## Scriptable Objects

Introduce use of Scriptable object to reduce scene dependencies.

## Scriptable Objects Plus

Introduce serialized and singleton Scriptable Objects.

## Model View Controller Service

Introduce Model-View-Controller-Service patterns in a way that compliments what we've already built.

___

# Micro-Demos

## Events Only

Example of using nothing but events for unity game.

## No Monobehaviour

Example of using no custom monobehaviours for a unity game.
___
# Game Design Brief

The game will be built out in a way that makes use of the expanding architectures.

## Monoscript
    
A single scene demo with a player, a gun that shoots bullets, enemies and an enemy spawner, Win and Lose state and a simple UI.

## Gameobject-Components

Add a main menu scene, 3 different levels, different enemy types, weapon pickups, health pickups and player and enemy health UI bars.

## Singletons

Replace direct references with Singletons.

## Interfaces

Increase reusability of object spawners and enemy types using Interfaces (give different enemies different logic)

## Unity-Events

Replace direct references and polling behaviour with Observable Event pattern. Use Unity Events.

## Coroutines / Await

Replace the _timeElapsed functions with reusable Couroutines are Awaits

## State

Replace ambigious Game Manager logic with an explicit state machine.

## Commands

Replace Unity Events with Commands. Add an instant replay feature, and a rewind feature.

## Scriptable Objects

Refactor much of the Monobehaviour and prefabs with scriptable objects. Expand Gun into generic Weapons.

## Scriptable Objects Plus

Add Serialized data for save / load functionality.

## Model View Controller Service

Add aspects of MVCS where it makes sense (UI, 3rd Party Services). Link to a 3rd party service.

# Topics to Touch On

- Monoscript (single monobehaviour)

- Gameobject-Components (classic)

- Single-Entry Point / Main Loop Pattern 
https://bronsonzgeb.com/index.php/2021/04/24/unity-architecture-pattern-the-main-loop/

- Type Object Pattern
https://bronsonzgeb.com/index.php/2021/09/17/the-type-object-pattern-with-scriptable-objects/

- Pooling Pattern

- Spacial Partition
https://www.habrador.com/tutorials/programming-patterns/19-spatial-partition-pattern/

- Additive Scene Loading 
https://www.gamedeveloper.com/disciplines/a-better-architecture-for-unity-projects

- Commands 
https://www.gamedeveloper.com/disciplines/a-better-architecture-for-unity-projects
https://bronsonzgeb.com/index.php/2021/09/25/the-command-pattern-with-scriptable-objects/

- Transactions 
https://www.gamedeveloper.com/disciplines/a-better-architecture-for-unity-projects

- Scriptable Object Asset Registery
https://bronsonzgeb.com/index.php/2021/09/11/the-scriptable-object-asset-registry-pattern/

- Model View Controller
https://bronsonzgeb.com/index.php/2021/05/08/model-view-controller-pattern-for-in-game-ui/

- Observable Variable
https://bronsonzgeb.com/index.php/2021/05/08/model-view-controller-pattern-for-in-game-ui/