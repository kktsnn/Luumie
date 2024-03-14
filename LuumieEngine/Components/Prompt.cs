using LuumieEngine.Classes;

namespace LuumieEngine.Components;

public class Prompt : Routine
{
    public UiNavigation? MainNav;
    public UiRoutine? First;

    private UiRoutine? _selected;
    // private bool _uiStatus = true;

    protected override void Awake()
    {
        GameEntity.ActiveSelf = false;
    }

    public virtual void Show()
    {
        GameEntity.ActiveSelf = true;

        // if (!UiNavigation.Active) _uiStatus = false;

        if (First == null) return;
        
        _selected = MainNav?.Current;
        if (MainNav != null) MainNav.Current = First;
    }

    public virtual void Hide()
    {
        GameEntity.ActiveSelf = false;
        
        // if (!_uiStatus)
        // {
        //     UiNavigation.Active = false;
        //     _uiStatus = true;
        // }
        
        if (MainNav != null) MainNav.Current = _selected;
    }
}