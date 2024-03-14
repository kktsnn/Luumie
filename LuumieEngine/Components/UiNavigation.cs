using LuumieEngine.Classes;

namespace LuumieEngine.Components;

public class UiNavigation : Routine
{
    private UiRoutine? _current;

    public UiRoutine? Current
    {
        get => _current;
        set
        {
            if (Enabled) _current?.OnDeselect();
            _current = value;
            if (Enabled) _current?.OnSelect();
        }
    }

    public static bool Active { get; set; }

    public event Action<EDirection>? Next;
    
    protected override void Awake()
    {
        Current?.OnSelect();
        Active = true;
    }

    protected override void OnKeyPress(ConsoleKeyInfo keyInfo)
    {
        if (Current == null || !Active || !Enabled) return;
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow or ConsoleKey.W:
                if (Current!.Up == null) return;
                Current = Current.Up;
                Next?.Invoke(EDirection.Up);
                break;
            case ConsoleKey.DownArrow or ConsoleKey.S:
                if (Current!.Down == null) return;
                Current = Current.Down;
                Next?.Invoke(EDirection.Down);
                break;
            case ConsoleKey.LeftArrow or ConsoleKey.A:
                if (Current!.Left == null) return;
                Current = Current.Left;
                Next?.Invoke(EDirection.Left);
                break;
            case ConsoleKey.RightArrow or ConsoleKey.D:
                if (Current!.Right == null) return;
                Current = Current.Right;
                Next?.Invoke(EDirection.Right);
                break;
            case ConsoleKey.Enter or ConsoleKey.Spacebar:
                if (!Current.Enabled) return;
                Current?.OnClick();
                break;
        }
    }
}

public enum EDirection
{
    Up, Down, Left, Right
}