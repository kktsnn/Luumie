using LuumieEngine.ScreenRendering;
using LuumieEngine.Structs;

namespace LuumieEngine.Classes;

public abstract class RenderRoutine : Routine
{
    public ConsoleColor Color = ConsoleColor.White;
    public Vector2Int Size;
    public Vector2Int LocalPosition;
    public Vector2Int Anchor => new (
        (Console.WindowWidth - Size.X) / 2 + Transform.Position.X + LocalPosition.X, 
        (Console.WindowHeight - Size.Y) / 2 - Transform.Position.Y - LocalPosition.Y);
    
    public abstract void Render();

    protected override void Update()
    {
        ScreenRenderer.Buffer.QueueJob(this);
    }
}