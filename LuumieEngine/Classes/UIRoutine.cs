using LuumieEngine.Components;

namespace LuumieEngine.Classes;

public abstract class UiRoutine : Routine
{
    private bool _active;
    private bool _pending;

    public bool Active
    {
        get => _active;
        set
        {
            _pending = value;
            LuumieManager.Update += UpdateActiveStatus;
        }
    }

    public void UpdateActiveStatus()
    {
        _active = _pending;
        LuumieManager.Update -= UpdateActiveStatus;
    }

    private UiRoutine? _up;
    public UiRoutine? Up
    {
        get => _up;
        set
        {
            _up = value;
            if (_up == null) return; 
            _up._down = this;
        }
    }

    private UiRoutine? _down;
    public UiRoutine? Down
    {
        get => _down;
        set
        {
            _down = value;
            if (_down == null) return; 
            _down._up = this;
        }
    }

    private UiRoutine? _left;
    public UiRoutine? Left
    {
        get => _left;
        set
        {
            _left = value;
            if (_left == null) return; 
            _left._right = this;
        }
    }

    private UiRoutine? _right;
    public UiRoutine? Right 
    { 
        get => _right;        
        set
        {
            _right = value;
            if (_right == null) return;
            _right._left = this;
        }
    }

    public RenderRoutine? Graphic;
    public ConsoleColor HighlightColor = ConsoleColor.White;
    private ConsoleColor _graphicNormalColor;

    public Text? Content;
    public ConsoleColor ClickColor = ConsoleColor.White;
    private ConsoleColor _clickNormalColor;

    public event Action? Selected;
    public virtual void OnSelect()
    {
        if (Graphic != null)
        {
            _graphicNormalColor = Graphic.Color;
            Graphic.Color = HighlightColor;
        }
        Selected?.Invoke();
    }

    public event Action? Deselected;
    public virtual void OnDeselect()
    {
        if (Graphic != null) Graphic.Color = _graphicNormalColor;
        Deselected?.Invoke();
    }

    public event Action? OnClicked;
    public virtual void OnClick()
    {
        if (Content != null) {
            _clickNormalColor = Content.Color;
            Content.Color = ClickColor;
        }
        OnClicked?.Invoke();
    }

    public event Action? Confirmed;
    public virtual void OnConfirm()
    {
        if (Content != null) Content.Color = _clickNormalColor;
        Confirmed?.Invoke();
    }

    public event Action? Canceled;
    public virtual void OnCancel()
    {
        if (Content != null) Content.Color = _clickNormalColor;
        Canceled?.Invoke();
    }

    protected override void OnDestroy()
    {
        Selected = null;
        Deselected = null;
        OnClicked = null;
        Confirmed = null;
        Canceled = null;
    }
}