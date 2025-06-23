using System;
using UnityEngine;

namespace Counters {
    public class CouttingCounterVisual : MonoBehaviour {
        private Animator anim;
        [SerializeField] private CuttingCounter counter;
        
        private void Awake() {
            anim = GetComponent<Animator>();
        }

        private void Start() {
            counter.OnCutting.AddListener(() => anim.SetTrigger(AnimParams.IsCutting));
        }
    }
}