using System;
using UnityEngine;

namespace Counters {
    public class ContainerCounterVisual : MonoBehaviour {
        private Animator anim;
        [SerializeField] private ContainerCounter counter;
        
        private void Awake() {
            anim = GetComponent<Animator>();
        }

        private void Start() {
            counter.OnPlayerGrabbedObject.AddListener((e) => anim.SetTrigger(AnimParams.IsGrabbed));
        }
    }
}