using LuumieEngine.Classes;
using LuumieEngine.ScreenRendering;
using LuumieEngine.Structs;

namespace LuumieEngine.Components;

public class Box : RenderRoutine
{
    public override void Render()
    {
        ScreenRenderer.Buffer.Write(
            Anchor,
            $".{new string('-', Size.X - 2)}.",
            Color
            );

        for (var i = 1; i < Size.Y - 1; i++)
        {
            // Add size calculation to anchor
            ScreenRenderer.Buffer.Write(Anchor + new Vector2Int(0, i), "|", Color);
            ScreenRenderer.Buffer.Write(Anchor + new Vector2Int(Size.X - 1, i), "|", Color);
        }
        
        ScreenRenderer.Buffer.Write(
            Anchor + new Vector2Int(0, Size.Y - 1),
            $"'{new string('-', Size.X - 2)}'",
            Color
        );
    }
}