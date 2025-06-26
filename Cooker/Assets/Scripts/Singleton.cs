using System;
using UnityEngine;

public interface ISingleton {
    void OnInitialize();
    void OnDestroy();
    virtual void OnUpdate() { }
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

public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;
    private static readonly object lockObj = new();
    private static bool isQuitting = false;

    public static bool IsInitialized => instance != null;

    public static T Instance {
        get {
            if (isQuitting) {
                Debug.LogWarning($"[SingletonMono] Instance of {typeof(T)} is requested after application quit.");
                return null;
            }

            lock (lockObj) {
                if (instance != null) return instance;

                if (instance == null) instance = FindAnyObjectByType<T>();
                if (instance != null) return instance;

                GameObject singletonObj = new GameObject(typeof(T).Name);
                instance = singletonObj.AddComponent<T>();
                DontDestroyOnLoad(singletonObj);
                Debug.Log($"[SingletonMono] Created singleton instance of {typeof(T)}.");
            }

            return instance;
        }
    }

    protected virtual void Awake() {
        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        } else if (instance != this) {
            Debug.LogWarning($"[SingletonMono] Duplicate instance of {typeof(T)} detected. Destroying the new one.");
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy() {
        if (instance == this) {
            instance = null;
        }
    }

    protected virtual void OnApplicationQuit() {
        isQuitting = true;
    }
}
