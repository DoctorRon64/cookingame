public interface ISingleton {
    void OnInitialize();
    void OnDestroy();
}

public class Singleton<T> where T : ISingleton, new() {
    private static T instance;
    
    public static T Instance {
        get {
            if (instance != null) return instance;
            instance = new();
            instance.OnInitialize();
            return instance;
        }
    }

    ~Singleton() {
        if (instance == null) return;
        instance.OnDestroy();
        instance = default;
    }
}
