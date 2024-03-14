using LuumieEngine.Classes;

namespace LuumieEngine.Components;

public class Button : UiRoutine
{
    public Action? Action { get; set; }

    public override void OnClick()
    {
        Action?.Invoke();
        base.OnClick();
        OnConfirm();
    }
}