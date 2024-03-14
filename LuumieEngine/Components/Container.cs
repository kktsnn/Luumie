using LuumieEngine.Classes;

namespace LuumieEngine.Components;

public class Container<T> : Routine
{
    public T? Content { get; set; }
}