using UnityEngine;

public abstract class SignalAssetBase : DataAsset {
    public abstract SignalBase BaseSignal { get; }
}

[CreateAssetMenu(menuName = "Asset/Signal/SignalAsset")]
public class SignalAsset : SignalAssetBase {
    public Signal Signal { get; private set; } = new(); 
    public void Invoke() => Signal.Invoke();
    public override SignalBase BaseSignal => Signal;
}

public class SignalAsset<T> : SignalAssetBase {
    public readonly Signal<T> Signal = new();
    public void Invoke(T value) => Signal.Invoke(value);
    public override SignalBase BaseSignal => Signal;
}