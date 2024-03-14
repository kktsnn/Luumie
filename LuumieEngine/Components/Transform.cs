using System.Collections;
using LuumieEngine.Classes;
using LuumieEngine.SceneManagement;
using LuumieEngine.Structs;

namespace LuumieEngine.Components;

public class Transform : Component, IEnumerable<Transform>
{
    public Transform? Parent { get; set; }
    private List<Transform> _children = new();
    public Transform this[int i] => _children[i];
    
    private Scene? _scene;
    public Scene? Scene {
        get => _scene ?? Parent?.Scene;
        set => _scene = value;
    }
    
    // Move away from localPosition
    public Vector2Int LocalPosition { get; set; }
    public Vector2Int Position {
        get => (Parent?.Position ?? Vector2Int.Zero) + LocalPosition;
        set => LocalPosition += value - Position;
    }

    public int LocalDepth { get; set; } = 1;
    public int Depth { 
        get => (Parent?.Depth ?? 0) + LocalDepth;
        set => LocalDepth += value - Depth;
    }

    internal override void Load()
    {
        _children.ForEach(e => e.GameEntity.Load());
    }

    internal override void Destroy()
    {
        _children.ForEach(e => e.GameEntity.Destroy());
        _children.Clear();
        GameEntity = null!;
    }

    public void AddChild(Transform e)
    {
        e.Transform.Parent = this;
        if (Scene?.Loaded ?? false) e.GameEntity.Load();
        _children.Add(e);
    }

    public void RemoveChild(Transform e)
    {
        _children.Remove(e);
        e.GameEntity.Destroy();
    }

    public void ClearChildren()
    {
        _children.ForEach(t => t.GameEntity.Destroy());
        _children.Clear();
    }

    public int ChildCount()
    {
        return _children.Count;
    }

    public int AllChildCount()
    {
        return ChildCount() == 0 ? 0 : ChildCount() + _children.Sum(c => c.AllChildCount());
    }

    public Transform? Find(string name)
    {
        return GameEntity.Name == name ? this : _children.Select(e => e.Find(name)).FirstOrDefault(r => r != null);
    }

    public IEnumerator<Transform> GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}