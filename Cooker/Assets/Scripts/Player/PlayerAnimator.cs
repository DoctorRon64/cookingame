using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private Animator anim;
    [SerializeField] private PlayerMovement PlayerMovement;
    
    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        anim.SetBool(AnimParams.IsWalking, PlayerMovement.IsWalking());
    }
}
