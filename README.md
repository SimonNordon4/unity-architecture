# Unity-Architectures

## Typical

Typical architectures are examples of the kind of architectures you would see in most Unity projects.

### Classic:

    Unity Gameobject-Component System where by Gameobjects directly reference other gameobjects in the Scene.

### Classic-Plus:

    Improves upon the classic architecture by introducing the Singleton Pattern, Object Pooling, Interfaces and improved coding standards.

### Classic-ScriptableObject

    Improves upoc Classic-Plus but uses Scriptable objects to reduce scene dependencies.

### AMVCC:

    Application-Model-View-Controller-Component Pattern for Unity.

### ECS:

    Entity-Component-System Pattern for Unity.

## Atypical

Atypical architectures are extreme examples highlighting certain pattern. You would not to expect to see entire projects made from these patterns, but they may employ heavy use of them.

In these examples, they go beyond sensible in sticking to their chosen architecture, as to better highlight their uses and downfalls.

### GodObject:

    A single scripts that controls almost all of the game.

### Unity Event Driven:

    Observer Pattern Architecture for Unity (Unity Events)
    
### Command Driven:

    Event-Driven with Commands (C# Events)

### No Monobehaviours

    Game without a single MonoBehaviour excluding pre-existing components.

### Pure MVC:

    Model-View-Controller Pattern for Unity.




