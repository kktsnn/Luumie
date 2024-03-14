using LuumieEngine.Components;

namespace LuumieEngine.Classes;

public abstract class Component
{
    public GameEntity GameEntity { get; internal set; } = null!;
    public Transform Transform => GameEntity.Transform;

    public T AddComponent<T>() where T : Routine
    {
        return GameEntity.AddComponent<T>();
    }
    
    public T GetComponent<T>() where T : Routine
    {
        return GameEntity.GetComponent<T>();
    }

    internal abstract void Load();

    internal abstract void Destroy();
}