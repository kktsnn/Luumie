using LuumieEngine.Classes;

namespace LuumieEngine.Components;

public class Dropdown : UiRoutine
{
    private string[] _options = Array.Empty<string>();
    public string[] Options
    {
        get => _options;
        set
        {
            _options = value;
            Select = 0;
        } 
    }
    
    private int _selected;
    private int _activeSelected;

    public int Select
    {
        private get => _selected;
        set
        {
            _selected = value;
            // _activeSelected = value;
            if (Content != null) 
                Content.Content = $"{(_selected > 0 ? "<" : " ")} {Current} {(_selected < _options.Length - 1 ? ">" : " ")}";
        }
    }

    public string Current => _options[_selected];

    protected override void OnKeyPress(ConsoleKeyInfo keyInfo)
    {
        if (!Active) return;
        
        switch (keyInfo.Key)
        {
            case ConsoleKey.LeftArrow or ConsoleKey.A:
                if (_selected > 0) Select--;
                break;
            case ConsoleKey.RightArrow or ConsoleKey.D:
                if (_selected < _options.Length - 1) Select++;
                break;
            case ConsoleKey.Enter or ConsoleKey.Spacebar:
                OnConfirm();
                return;
            case ConsoleKey.Escape:
                OnCancel();
                break;
        }
    }
    
    public override void OnClick()
    {
        Active = true;
        UiNavigation.Active = false;
        base.OnClick();
    }

    public override void OnConfirm()
    {
        _activeSelected = _selected;
        OffClick();
        base.OnConfirm();
    }

    public override void OnCancel()
    {
        Select = _activeSelected;
        OffClick();
        base.OnCancel();
    }

    private void OffClick()
    {
        Active = false;
        UiNavigation.Active = true;
    }
}