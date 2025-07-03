using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
    public StateMachine<GameManager> GameStateMachine { get; private set; }
    public Signal<int> OnCountDownStartChanged { get; internal set; } = new();
    
    [field: SerializeField] public float AwaitStartTimer { get; internal set; } = 1f;
    [field: SerializeField] public float CountdownStartTimer { get; internal set; } = 3f;
    [field: SerializeField] public float GamePlayingTimer { get; internal set; } 
    [field: SerializeField] public float GamePlayingTimerMax { get; internal set; } = 10f;
    
    protected override void Awake() {
        base.Awake();

        GameStateMachine = new(this);
        GameStateMachine.Add<GameAwaitStartState>();
        GameStateMachine.Add<GameCountdownState>();
        GameStateMachine.Add<GamePlayingState>();
        GameStateMachine.Add<GameOverState>();
        GameStateMachine.Switch<GameAwaitStartState>();
        
        
        _ = InputManager.Instance;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        GameStateMachine.OnStateChanged.Clear();
        OnCountDownStartChanged.Clear();
    }

    private void Update() {
        GameStateMachine.OnUpdate(); 
    }
    
    public bool IsGamePlaying() => GameStateMachine.CurrentState is GamePlayingState;
    public bool IsCountdownAwaitActive() => GameStateMachine.CurrentState is GameCountdownState;   
    public bool IsGameOver() => GameStateMachine.CurrentState is GameOverState;
    public float GetGamePlayingTimerNormalized() => 1 - (GamePlayingTimer / GamePlayingTimerMax);
}

public class GameAwaitStartState : BaseState<GameManager> {
    public override void OnUpdate() {
        Blackboard.AwaitStartTimer -= Time.deltaTime;
        if (Blackboard.AwaitStartTimer <= 0f) {
            StateMachine.Switch<GameCountdownState>();
        }
    }
}
public class GameCountdownState : BaseState<GameManager> {
    public override void OnUpdate() {
        Blackboard.CountdownStartTimer -= Time.deltaTime;
        int currentSecond = Mathf.RoundToInt(Blackboard.CountdownStartTimer);
        Blackboard.OnCountDownStartChanged?.Invoke(currentSecond);
        
        if (Blackboard.CountdownStartTimer <= 0f) {
            StateMachine.Switch<GamePlayingState>();
        }
    }
}
public class GamePlayingState : BaseState<GameManager> {
    public override void OnUpdate() {
        Blackboard.GamePlayingTimer -= Time.deltaTime;
        if (Blackboard.GamePlayingTimer <= 0f) {
            StateMachine.Switch<GameOverState>();
        }
    }

    public override void OnExit() {
        Blackboard.GamePlayingTimer = Blackboard.GamePlayingTimerMax;
    }
}
public class GameOverState : BaseState<GameManager> { }
