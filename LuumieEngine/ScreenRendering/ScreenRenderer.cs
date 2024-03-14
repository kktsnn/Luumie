namespace LuumieEngine.ScreenRendering;

internal static class ScreenRenderer
{
    internal static readonly ScreenBuffer Buffer = new ();

    internal static void Initialize()
    {
        Buffer.ChangeBufferSize(Console.WindowWidth, Console.WindowHeight);
        Listener.WindowSizeChanged += () =>
        {
            Buffer.ChangeBufferSize(Console.WindowWidth, Console.WindowHeight);
        };
        LuumieManager.LateUpdate += Buffer.Render_Extern;
    }
}