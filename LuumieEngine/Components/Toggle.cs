using LuumieEngine.Classes;

namespace LuumieEngine.Components;

public class Toggle : UiRoutine
{
    public override void OnClick()
    {
        Active = !Active;
        if (Content != null) Content.Content = Active ? "On" : "Off";
        base.OnClick();
    }
}