namespace LuumieEngine.Classes;

public class Routine : Component
{
    private bool _enabledSelf = true;
    private bool _pending;

    public bool EnabledSelf
    {
        get => _enabledSelf;
        set
        {
            _pending = value;
            LuumieManager.Update += UpdateStatus;
        }
    }

    public void UpdateStatus()
    {
        _enabledSelf = _pending;
        LuumieManager.Update -= UpdateStatus;
    }

    public bool Enabled => EnabledSelf && GameEntity.Active;
    

    protected static void StartCoroutine(IEnumerator<int> r)
    {
        Coroutine.StartCoroutine(r);
    }

    internal override void Load()
    {
        LuumieManager.Update += e_Update;
        Listener.KeyPressed += e_OnKeyPress;
        Awake();
    }
    
    protected virtual void Awake()
    {
    }

    internal override void Destroy()
    {
        LuumieManager.Update -= e_Update;
        Listener.KeyPressed -= e_OnKeyPress;
        OnDestroy();
    }

    protected virtual void OnDestroy()
    {
    }
    
    internal void e_Update()
    {
        if (Enabled) Update();
    }
    
    protected virtual void Update()
    {
    }

    internal void e_OnKeyPress(ConsoleKeyInfo keyInfo)
    {
        if (Enabled) OnKeyPress(keyInfo);
    }
    
    protected virtual void OnKeyPress(ConsoleKeyInfo keyInfo)
    {
    }
}