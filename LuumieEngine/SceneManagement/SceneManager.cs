using LuumieEngine.Classes;

namespace LuumieEngine.SceneManagement;

public delegate void SceneChange(Scene? scene);

public static class SceneManager
{
    private static Scene? _activeScene;

    public static event Action? SceneLoaded;

    private static Type? _nextScene;
    private static bool _changeScene;

    internal static void Initialize()
    {
        LuumieManager.EarlyUpdate += Change;
    }
    
    public static void ChangeScene(Type? type)
    {
        _nextScene = type;
        _changeScene = true;
    }

    private static void Change()
    {
        if (!_changeScene) return;
        
        Coroutine.StopAllCoroutines();
        
        _activeScene?.Unload();
        
        GC.Collect();
        
        if (_nextScene == null) return;
        
        var s = Activator.CreateInstance(_nextScene);
        
        if (s is not Scene scene) throw new Exception("No Scene of Type: " + _nextScene);

        _activeScene = scene;
        
        scene.Load();

        _changeScene = false;
        
        SceneLoaded?.Invoke();
    }
}