using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class GameStartCountdownUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private string[] localizedKeys = new string[4];

    private void Start() {
        gameObject.SetActive(false);
        
        GameManager.Instance.GameStateMachine.OnStateChanged.AddListener(GameManager_OnGameStateChanged);
        GameManager.Instance.OnCountDownStartChanged.AddListener(OnCountdownValueChanged);
    }

    private void OnDestroy() {
        GameManager.Instance.GameStateMachine.OnStateChanged.RemoveListener(GameManager_OnGameStateChanged);
        GameManager.Instance.OnCountDownStartChanged.RemoveListener(OnCountdownValueChanged);
    }

    private void OnCountdownValueChanged(int index) {
        index = Mathf.Clamp(index, 0, localizedKeys.Length - 1);
        countdownText.text = new LocalizedString("UIText", localizedKeys[index]).GetLocalizedString();
    }

    private void GameManager_OnGameStateChanged(IState<GameManager> state) {
        gameObject.SetActive(GameManager.Instance.IsCountdownAwaitActive());
    }
}