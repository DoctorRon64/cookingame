using Counters;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private GameObject hasProgressObj;
    [SerializeField] private Image progressBar;
    private IHasProgress hasProgress;
    
    private void Start() {
        hasProgress = hasProgressObj.GetComponent<IHasProgress>();
        hasProgress.OnProgressChanged.AddListener((nStruct) => ProgressChanged(nStruct.ProgressNormalizeFloat));
        progressBar.fillAmount = 0f;
        UpdateVisibility(false);
    }

    private void ProgressChanged(float value) {
        progressBar.fillAmount = value;

        if (value == 0f || Mathf.Approximately(value, 1f)) {
            UpdateVisibility(false);
        }
        else {
            UpdateVisibility(true);
        }
    }
    
    private void UpdateVisibility(bool isVisible) => gameObject.SetActive(isVisible);
}