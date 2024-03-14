using LuumieEngine.Classes;
using LuumieEngine.ScreenRendering;
using LuumieEngine.Structs;

namespace LuumieEngine.Components;

public class Text : RenderRoutine
{
    private string _text = "";
    public string Content { 
        get => _text;
        set
        {
            _text = value; 
            RecalculateSize();
        }
    }

    public ETextAlignment Alignment = ETextAlignment.Middle;

    private void RecalculateSize()
    {
        Size = new Vector2Int(_text.Split('\n').Max(l => l.Length), _text.Count(c => c == '\n') + 1);
    }

    public override void Render()
    {
        var top = Anchor.Y;
        foreach (var line in _text.Split('\n'))
        {
            ScreenRenderer.Buffer.Write(
                new Vector2Int(Anchor.X + (Size.X - line.Length) / 2 * (int)Alignment, top++), 
                line, Color);
        }
    }
}

public enum ETextAlignment
{
    Left, Middle, Right
}