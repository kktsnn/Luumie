using LuumieEngine.Classes;
using LuumieEngine.ScreenRendering;

namespace LuumieEngine.Components;

public class AsciiImage : RenderRoutine
{
    private List<string> _content = new();
    private string? _path;

    public string? Path
    {
        get => _path;
        set
        {
            _path = value;
            Awake();
            RecalculateSize();
        }
    }

    protected override void Awake()
    {
        if (Path == null) return;
        
        _content.Clear();
        
        using var r = new StreamReader(Path);
        
        while (r.ReadLine() is { } line)
        {
            _content.Add(line.TrimEnd('\n'));
        }
    }
    
    private void RecalculateSize()
    {
        if (Path == null) return;

        var h = 0;
        foreach (var line in _content)
        {
            if (line.Length > Size.X) Size.X = line.Length;
            h++;
        }

        Size.Y = h;
    }


    public override void Render()
    {
        var top = Anchor.Y;

        foreach (var line in _content)
        {
            ScreenRenderer.Buffer.Write(Anchor with { Y = top++ },line.TrimEnd('\n'), Color);   
        }
    }
}