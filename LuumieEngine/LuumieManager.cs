using LuumieEngine.SceneManagement;
using LuumieEngine.ScreenRendering;

namespace LuumieEngine;

public static class LuumieManager
{
    internal static event Action? EarlyUpdate;
    internal static event Action? Update;
    internal static event Action? LateUpdate;

    private static bool _alive;

    public static int FrameRate = 10;
    
    public static void Start()
    {
        _alive = true;

        ScreenRenderer.Initialize();
        Listener.Initialize();
        SceneManager.Initialize();
        
        var fc = new System.Diagnostics.Stopwatch();

        while (_alive)
        {
            fc.Restart();
            
            EarlyUpdate?.Invoke();
            
            Update?.Invoke();
            
            LateUpdate?.Invoke();
            
            // capped framerate
            var sleep = 1000 / FrameRate - (int)fc.Elapsed.TotalMilliseconds + 1;
            if (sleep > 0) Thread.Sleep(sleep);
        }
        
        Console.Clear();
        foreach (var c in "Shutting Down...".ToCharArray())
        {
            Console.Write(c);
            Thread.Sleep(50);
        }
    }

    public static void Quit()
    {
        _alive = false;
        Listener.Kill();
    }
}