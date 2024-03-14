using LuumieEngine.Classes;
using LuumieEngine.Components;

namespace LuumieEngine.SceneManagement;

public abstract class Scene
{
    public bool Loaded { get; set; }
    
    private readonly List<GameEntity> _entities = new();
    public List<GameEntity> Entities => _entities;
    
    protected GameEntity AddEntity(GameEntity e)
    {
        e.Transform.Scene = this;
        _entities.Add(e);
        return e;
    }
    
    protected UiNavigation AddUiSystem()
    {
        return AddEntity(GameEntity.Empty).AddComponent<UiNavigation>();
    }

    public void Load()
    {
        _entities.ForEach(e => e.Load());
        Loaded = true;
    }

    public void Unload()
    {
        Loaded = false;
        _entities.ForEach(e => e.Destroy());
    }
}