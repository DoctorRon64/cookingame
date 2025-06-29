using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {
    [SerializeField] private float footstepTimerMax = .1f;
    [SerializeField] private float volume = 0.5f;
    
    private Player player;
    private float footstepTimer;
    
    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if (!(footstepTimer <= 0)) return;
        footstepTimer = footstepTimerMax;

        if (!player.IsWalking) return;
        SoundManager.Instance.PlayerFootsteps(player.transform.position, volume);
    }
}
