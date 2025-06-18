public interface ISingletonInit {
    protected internal void OnInitialize();
}

public class Singleton<T> where T : new() {

    public static T Instance {
        get {
            if (instance != null) return instance;
            instance = new T();
            return instance;
        }
    }

    private static T instance;
}

public class SingletonInit<T> where T : ISingletonInit, new() {

    public static T Instance {
        get {
            if (instance != null) return instance;
            instance = new T();
            instance.OnInitialize();
            return instance;
        }
    }

    private static T instance;
}