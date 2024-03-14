namespace LuumieEngine.Classes;

public class Coroutine
{
    private static List<Coroutine> _activeCoroutines = new();
    
    private readonly IEnumerator<int> _coroutine;
    
    private int _framesToWait;

    private Coroutine(IEnumerator<int> coroutine)
    {
        _coroutine = coroutine;

        if (!_coroutine.MoveNext()) return;
        
        LuumieManager.EarlyUpdate += Update;
        _framesToWait = _coroutine.Current;
    }
    
    private void Update()
    {
        if (_framesToWait != 0) _framesToWait--;
        else if (!_coroutine.MoveNext()) Stop();
        else _framesToWait = _coroutine.Current;
    }

    private void Stop()
    {
        LuumieManager.EarlyUpdate -= Update;
    }

    internal static void StartCoroutine(IEnumerator<int> e)
    {
        _activeCoroutines.Add(new Coroutine(e));
    }

    internal static void StopAllCoroutines()
    {
        _activeCoroutines.ForEach(c => c.Stop());
        _activeCoroutines.Clear();
    }
}