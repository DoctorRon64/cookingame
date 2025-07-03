using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dishesDeliveredText;

    private void Start() {
        gameObject.SetActive(false);
        
        DeliveryManager.Instance.OnRecipeSuccess.AddListener(DeliveryManager_OnRecipeSuccess);
        GameManager.Instance.GameStateMachine.OnStateChanged.AddListener(GameManager_OnGameStateChanged);
    }

    private void DeliveryManager_OnRecipeSuccess(int index) {
        dishesDeliveredText.text = index.ToString();
    }

    private void GameManager_OnGameStateChanged(IState<GameManager> state) {
        gameObject.SetActive(state is GameOverState);
    }
}
