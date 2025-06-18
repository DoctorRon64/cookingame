using System;
using System.Collections.Generic;

public abstract class SignalBase {
    public delegate void signalDelegate();
    public delegate void signalDelegate<T>(T data);
    public delegate void signalDelegate<T, U>(T data, U data2);

    protected List<Delegate> listeners = new();

    internal void AddListener(Delegate listener) {
        if (!listeners.Contains(listener)) {
            listeners.Add(listener);
        }
    }

    internal void RemoveListener(Delegate listener) {
        listeners.Remove(listener);
    }

    internal void Clear() {
        listeners.Clear();
    }
}

public class Signal : SignalBase {
    public void Invoke() {
        foreach (Delegate listener in listeners) {
            listener.DynamicInvoke();
            //((Action)listener)?.Invoke();
        }
    }
}

public class Signal<T> : SignalBase {
    public void AddListener(Action<T> listener) => base.AddListener(listener);
    public void RemoveListener(Action<T> listener) => base.RemoveListener(listener);
    
    public void Invoke(T parameter) {
        foreach (Delegate listener in listeners) {
            listener.DynamicInvoke(parameter);
            //((Action<T>)listener)?.Invoke(parameter);
        }
    }
}

public class Signal<T, U> : SignalBase {
    public void Invoke(T parameter, U parameter2) {
        foreach (Delegate listener in listeners) {
            listener.DynamicInvoke(parameter, parameter2);
            //((Action<T, U>)listener)?.Invoke(parameter, parameter2);
        }
    }
}