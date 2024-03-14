namespace LuumieEngine;

public delegate void KeyPress(ConsoleKeyInfo keyInfo);

public static class Listener
{
    internal static event KeyPress? KeyPressed;
    public static event Action? WindowSizeChanged;

    private static readonly Queue<ConsoleKeyInfo> KeyQueue = new();
    private static bool _sizeChanged;
    private static int _width;
    private static int _height;

    private static bool _alive;
    
    internal static void Initialize()
    {
        _alive = true;
        var thread = new Thread(Loop) { IsBackground = true };
        thread.Start();
        LuumieManager.EarlyUpdate += DequeueEvents;
    }

    internal static void Kill()
    {
        _alive = false;
    }
    
    private static void Loop()
    {
        while (_alive)
        {
            Console.SetCursorPosition(0, 0);
            if (Console.KeyAvailable) KeyQueue. Enqueue(Console.ReadKey(true));
            
            if (_width == Console.WindowWidth && _height == Console.WindowHeight) continue;
            
            _width = Console.WindowWidth;
            _height = Console.WindowHeight;
            _sizeChanged = true;
        }
    }

    private static void DequeueEvents()
    {
        var keys = KeyQueue.ToArray();
        KeyQueue.Clear();
        foreach (var e in keys) KeyPressed?.Invoke(e);
        
        if (!_sizeChanged) return;
        WindowSizeChanged?.Invoke();
        _sizeChanged = false;
    }
}