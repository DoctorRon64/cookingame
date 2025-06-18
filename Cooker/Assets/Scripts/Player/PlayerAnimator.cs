using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private Animator anim;
    [SerializeField] private Player Player;
    
    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        anim.SetBool(AnimParams.IsWalking, Player.IsWalking);
    }
}