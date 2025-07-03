using System;
using Counters;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoSingleton<SoundManager> {
    [SerializeField] private AudioAssetLib library;

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess.AddListener(DeliveryManager_OnRecipeSuccess);
        DeliveryManager.Instance.OnRecipeFailed.AddListener(DeliveryManager_OnRecipeFailed);
        CuttingCounter.OnAnyCut.AddListener(CuttingCounter_OnAnyCut);
        Player.Instance.OnPickedSomething.AddListener(Player_OnPickedSomething);
        BaseCounter.OnAnyObjectPlacedHere.AddListener(BaseCounter_OnAnyObjectPlacedHere);
        TrashCounter.OnAnyObjectTrashed.AddListener(TrashCounters_OnAnyObjectTrashed);
    }

    private void DeliveryManager_OnRecipeSuccess(int value) {
        DeliveryCounter counter = DeliveryCounter.Instance;
        PlaySound(library.DeliverySuccess, counter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed() {
        DeliveryCounter counter = DeliveryCounter.Instance;
        PlaySound(library.DeliveryFail, counter.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender) {
        CuttingCounter counter = sender as CuttingCounter;
        if (counter != null) PlaySound(library.Chop, counter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender) {
        BaseCounter counter = sender as BaseCounter;
        if (counter != null) PlaySound(library.ObjectDrop, counter.transform.position);
    }

    private void TrashCounters_OnAnyObjectTrashed(object sender) {
        TrashCounter counter = sender as TrashCounter;
        if (counter != null) PlaySound(library.Trash, counter.transform.position);
    }
    
    private void Player_OnPickedSomething() {
        PlaySound(library.ObjectPickup, Player.Instance.transform.position);
    }
    
    public void PlayerFootsteps(Vector3 position, float volume = 1f) {
        PlaySound(library.Footstep, position, volume);
    }

    private void PlaySound(AudioAsset asset, Vector3 position, float volume = 1f) {
        PlaySound(asset.Audio[Random.Range(0, asset.Audio.Length)], position, volume);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}