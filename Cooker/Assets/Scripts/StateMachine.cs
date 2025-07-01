using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> {
    private IState<T> currentState;
    public IState<T> CurrentState => currentState;
    private readonly Dictionary<Type, IState<T>> allStates = new Dictionary<Type, IState<T>>();
    public T Blackboard { get; private set; }
    
    public Signal<IState<T>> OnStateChanged { get; private set; }
    
    public StateMachine(T blackboard) {
        Blackboard = blackboard;
        OnStateChanged = new();
    }

    public void OnFixedUpdate() {
        currentState?.OnFixedUpdate();
    }

    public void OnUpdate() {
        currentState?.OnUpdate();
    }

    public void Switch<U>() where U : IState<T> {
        allStates.TryGetValue(typeof(U), out IState<T> currentState);
        if (currentState == null) return;
        currentState?.OnExit();
        this.currentState = currentState;
        Debug.Log("Switch to state: " + currentState.ToString());
        OnStateChanged?.Invoke(this.currentState);
        currentState.OnEnter();
    }

    public void Add<U>() where U : IState<T>, new() {
        if (allStates.ContainsKey(typeof(U))) return;
        IState<T> stateInstance = new U();
        allStates.Add(typeof(U), stateInstance);
        Debug.Log("initalize State: " + stateInstance);
        stateInstance.OnInitialize(this);
    }

    public void Add<U>(U stateInstance) where U : IState<T> {
        Type stateType = typeof(U);
        if (allStates.ContainsKey(stateType)) return;
        allStates.Add(stateType, stateInstance);
        Debug.Log("initalize State: " + stateInstance);
        stateInstance.OnInitialize(this);
    }

    public void RemoveState<U>() {
        if (!allStates.ContainsKey(typeof(U))) return;
        allStates.Remove(typeof(U));
    }
}

public interface IState<T> {
    public void OnInitialize(StateMachine<T> owner);
    public void OnEnter();
    public void OnExit();
    public void OnUpdate();
    public void OnFixedUpdate();
}

public abstract class BaseState<T> : IState<T> {
    protected StateMachine<T> StateMachine { get; private set; }
    protected T Blackboard => StateMachine.Blackboard;

    public virtual void OnInitialize(StateMachine<T> owner) {
        this.StateMachine = owner;
    }

    public virtual void OnEnter(){ }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
}