using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveCounterVisual;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private StoveCounter stove;

    private void Start() {
        stove.StoveStateMachine.OnStateChanged.AddListener(OnStateChange);
    }

    private void OnStateChange(IState<StoveCounter> state) {
        bool showVisual = state is StoveFryingState or StoveFriedState;
        stoveCounterVisual.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
