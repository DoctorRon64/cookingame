using System;
using UnityEditor.VersionControl;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    [SerializeField] private ClearCounter counter;
    [SerializeField] private GameObject counterVisual;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged.AddListener(OnSelectCounterChanged);
    }

    private void OnSelectCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        Debug.Log(sender?.ToString(), e.selectedCounter);
        ActivatedObject(e.selectedCounter == counter);
    }

    private void ActivatedObject(bool value) => counterVisual.SetActive(value);
}