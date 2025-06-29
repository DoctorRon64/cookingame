using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
    [SerializeField] private StoveCounter stove;
    private AudioSource source;

    private void Awake() {
        source = GetComponent<AudioSource>();
    }

    private void Start() {
        stove.StoveStateMachine.OnStateChanged.AddListener(StoveCounter_StoveStateChanged);
    }

    private void StoveCounter_StoveStateChanged(IState<StoveCounter> state) {
        bool playSound = state is StoveFryingState or StoveFriedState;
        if (playSound) {
            source.Play();
        } else {
            source.Pause();
        }
    }
}