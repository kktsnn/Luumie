using LuumieEngine.Components;
using LuumieEngine.Structs;

namespace LuumieEngine.Classes;

public class GameEntity
{
    public string? Name { get; set; }
    
    private List<Component> _components = new();
    public Transform Transform { get; private set; } = new();

    public bool ActiveSelf { get; set; } = true;
    public bool Active => ActiveSelf && (Transform?.Parent?.GameEntity?.Active ?? true) &&
                          (Transform?.Scene?.Loaded ?? false);

    private bool? _pending;
    
    private GameEntity()
    {
        Transform.GameEntity = this;
    }

    public void Load()
    {
        _components.ForEach(c => c.Load());
        Transform.Load();
        LuumieManager.Update += Update;
    }

    public void Destroy()
    {
        LuumieManager.Update -= Update;
        _components.ForEach(c => c.Destroy());
        _components.Clear();
        Transform?.Destroy();
        Transform = null!;
    }

    public void Update()
    {
        if (_pending != null) ActiveSelf = _pending.Value;
    }
    
    public T AddComponent<T>() where T : Routine
    {
        var c = (T)Activator.CreateInstance(typeof(T))!;
        c.GameEntity = this;
        if (Transform.Scene?.Loaded ?? false) c.Load();
        _components.Add(c);
        return c;
    }

    public T GetComponent<T>() where T : Component
    {
        foreach (var c in _components)
        {
            if (c is T component) return component;
        }
        
        throw new Exception("No component of type: " + typeof(T));
    }

    public void EnableComponents()
    {
        foreach (var c in _components)
        {
            if (c is Routine r) r.EnabledSelf = true;
        }
    }

    public void AddChild(GameEntity e)
        => Transform?.AddChild(e.Transform);

    public void RemoveChild(GameEntity e)
        => Transform?.RemoveChild(e.Transform);

    public void ClearChildren()
        => Transform?.ClearChildren();

    public int ChildCount()
        => Transform?.ChildCount() ?? 0;

    public static GameEntity Empty => new() { Name = "Empty" };

    public static GameEntity InputField
    {
        get
        {
            var e = Empty;
            var field = e.AddComponent<InputField>();
            field.Placeholder = e.AddComponent<Text>();
            field.Content = e.AddComponent<Text>();
            field.Graphic = e.AddComponent<Box>();
            field.Graphic.Size = new Vector2Int(22, 3);
            return e;
        }
    }

    public static GameEntity Dropdown
    {
        get
        {
            var e = Empty;
            var dropdown = e.AddComponent<Dropdown>();
            dropdown.Content = e.AddComponent<Text>();
            dropdown.Graphic = e.AddComponent<Box>();
            dropdown.Graphic.Size = new Vector2Int(22, 3);
            return e;
        }
    }

    public static GameEntity Button
    {
        get
        {
            var e = Empty;
            var button = e.AddComponent<Button>();
            button.Content = e.AddComponent<Text>();
            button.Content.Content = "Button";
            button.Graphic = e.AddComponent<Box>();
            button.Graphic.Size = new Vector2Int(22, 3);
            return e;
        }
    }

    public static GameEntity Toggle
    {
        get
        {
            var e = Empty;
            var toggle = e.AddComponent<Toggle>();
            var text = e.AddComponent<Text>();
            text.Content = "Toggle:";
            toggle.Graphic = text;
            toggle.Content = e.AddComponent<Text>();
            toggle.Content.LocalPosition += new Vector2Int(6, 0);
            toggle.Content.Content = "Off";
            return e;
        }
    }

    public static GameEntity Text
    {
        get
        {
            var e = Empty;
            e.AddComponent<Text>();
            return e;
        }
    }
    
    public static GameEntity Image
    {
        get
        {
            var e = Empty;
            e.AddComponent<AsciiImage>();
            return e;
        }
    }
    
    public static GameEntity Background
    {
        get
        {
            var e = Empty;
            e.AddComponent<Background>();
            return e;
        }
    }
    
    public static GameEntity Box
    {
        get
        {
            var e = Empty;
            e.AddComponent<Box>();
            return e;
        }
    }
}