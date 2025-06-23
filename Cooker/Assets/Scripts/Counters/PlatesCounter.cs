using System;
using KitchenObjects;
using UnityEngine;

namespace Counters {
    public class PlatesCounter : BaseCounter {
        public readonly Signal OnPlateSpawned = new Signal();
        public readonly Signal OnPlateRemoved = new Signal();
        
        [SerializeField] private KitchenObjectAsset plateAsset;
        [SerializeField] private float spawnPlateTimerMax;
        [SerializeField] private int platesMax = 5;
        
        private float spawnPlateTimer;
        private int platesAmount;

        private void Update() {
            spawnPlateTimer += Time.deltaTime;
            if (!(spawnPlateTimer > spawnPlateTimerMax)) return;
            spawnPlateTimer = 0f;

            if (platesAmount >= platesMax) return;
            platesAmount++;
                    
            OnPlateSpawned?.Invoke();
        }

        private void OnDestroy() {
            OnPlateSpawned?.Clear();
            OnPlateRemoved?.Clear();
        }

        public override void Interact(Player player) {
            if (!player.HasKitchenObject()) {
                if (platesAmount > 0) {
                    platesAmount--;
                    
                    KitchenObject.SpawnKitchenObject(plateAsset, player);
                    OnPlateRemoved?.Invoke();
                }
            }   
        }
    }
}