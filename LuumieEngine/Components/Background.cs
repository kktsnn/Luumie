using LuumieEngine.Classes;
using LuumieEngine.ScreenRendering;

namespace LuumieEngine.Components;

public class Background : RenderRoutine
{
    public new ConsoleColor Color = ConsoleColor.Black;

    public override void Render()
    {
        ScreenRenderer.Buffer.Background(Anchor, Size, Color);
    }
}