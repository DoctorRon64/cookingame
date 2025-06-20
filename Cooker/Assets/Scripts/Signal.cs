using System;
using System.Collections.Generic;

public abstract class SignalBase {
    protected readonly List<Delegate> Listeners = new();

    internal void AddListener(Delegate listener) {
        if (listener != null && !Listeners.Contains(listener))
            Listeners.Add(listener);
    }

    internal void RemoveListener(Delegate listener) {
        if (listener != null)
            Listeners.Remove(listener);
    }

    internal void Clear() {
        Listeners.Clear();
    }
}

public class Signal : SignalBase {
    public void AddListener(Action callback) {
        AddListener((Delegate)callback);
    }

    public void RemoveListener(Action callback) {
        RemoveListener((Delegate)callback);
    }

    public void Invoke() {
        foreach (Delegate listener in Listeners) {
            (listener as Action)?.Invoke();
        }
    }
}

public class Signal<T> : SignalBase {
    public void AddListener(Action<T> callback) {
        AddListener((Delegate)callback);
    }

    public void RemoveListener(Action<T> callback) {
        RemoveListener((Delegate)callback);
    }

    public void Invoke(T arg) {
        foreach (Delegate listener in Listeners) {
            (listener as Action<T>)?.Invoke(arg);
        }
    }
    
    public void Invoke(object sender, T arg) {
        foreach (Delegate listener in Listeners) {
            (listener as EventHandler<T>)?.Invoke(sender, arg);
        }
    }

    public void AddListener(EventHandler<T> callback) {
        AddListener((Delegate)callback);
    }
}

public class Signal<T, U> : SignalBase {
    public void AddListener(Action<T, U> callback) {
        AddListener((Delegate)callback);
    }

    public void RemoveListener(Action<T, U> callback) {
        RemoveListener((Delegate)callback);
    }

    public void Invoke(T arg1, U arg2) {
        foreach (Delegate listener in Listeners) {
            (listener as Action<T, U>)?.Invoke(arg1, arg2);
        }
    }
}