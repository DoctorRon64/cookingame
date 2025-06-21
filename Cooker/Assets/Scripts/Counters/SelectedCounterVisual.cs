using UnityEngine;

namespace Counters {
    public class SelectedCounterVisual : MonoBehaviour {
        [SerializeField] private BaseCounter baseCounter;
        [SerializeField] private GameObject[] visualGameObjects;

        private void Start() {
            Player.Instance.OnSelectedCounterChanged.AddListener(OnSelectCounterChanged);
        }

        private void OnSelectCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
            //Debug.Log(sender?.ToString(), e.selectedCounter);
            ActivatedObject((BaseCounter)e.HighlightedCounter == baseCounter);
        }

        private void ActivatedObject(bool value) {
            foreach (GameObject obj in visualGameObjects) {
                obj.SetActive(value);
            }
        } 
    }
}