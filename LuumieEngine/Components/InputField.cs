using LuumieEngine.Classes;

namespace LuumieEngine.Components;

public class InputField : UiRoutine
{
    public Text? Placeholder { get; set; }
    public int Limit { get; set; } = 20;
    
    private LinkedList<char> _content = new();
    private char[] _activeContent = Array.Empty<char>();
    
    public string Current
    {
        get => string.Join("", _content);
        set
        {
            _content = new LinkedList<char>(value);
            _activeContent = value.ToCharArray();
            if (Content != null) Content.Content = Current;
        }
    }
    
    protected override void OnKeyPress(ConsoleKeyInfo keyInfo)
    {
        if (!Active) return;
        switch (keyInfo.Key)
        {
            case ConsoleKey.Backspace when _content.Count > 0:
                _content.RemoveLast();
                break;
            case ConsoleKey.Enter:
                OnConfirm();
                return;
            case ConsoleKey.Escape:
                OnCancel();
                break;
            default:
                var key = keyInfo.KeyChar;
                if (char.IsControl(key) || _content.Count == Limit) return;
                _content.AddLast(key);
                break;
        }

        if (Content != null)
        {
            Content.Content = Current;
            Content.Size.X = Limit;
        }
    }

    public override void OnClick()
    {
        Active = true;
        UiNavigation.Active = false;
        if (Placeholder != null) Placeholder.EnabledSelf = false;
        base.OnClick();
    }

    public override void OnConfirm()
    {
        _activeContent = new char[_content.Count];
        _content.CopyTo(_activeContent, 0);
        OffClick();
        if (Placeholder != null) Placeholder.EnabledSelf = _content.Count == 0;
        base.OnConfirm();
    }

    public override void OnCancel()
    {
        _content.Clear();
        foreach (var c in _activeContent)
            _content.AddLast(c);
        OffClick();
        base.OnCancel();
    }
    
    private void OffClick()
    {
        Active = false;
        UiNavigation.Active = true;
    }
}