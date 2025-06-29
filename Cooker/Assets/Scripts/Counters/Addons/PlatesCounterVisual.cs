using System;
using System.Collections.Generic;
using Counters;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
    [SerializeField] private PlatesCounter counter;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private Vector3 plateVisualOffset;
    
    private readonly List<GameObject> plateVisuals = new();
    
    private void Start() {
        counter.OnPlateSpawned.AddListener(OnPlateSpawned);
        counter.OnPlateRemoved.AddListener(OnPlatesRemoved);
    }

    private void OnPlateSpawned() {
        Transform plateVisual = Instantiate(plateVisualPrefab, topPoint.position, Quaternion.identity, topPoint);

        plateVisual.position = topPoint.position + plateVisualOffset * plateVisuals.Count;
        plateVisuals.Add(plateVisual.gameObject);   
    }

    private void OnPlatesRemoved() {
        GameObject plateObj = plateVisuals[plateVisuals.Count - 1];
        plateVisuals.Remove(plateObj);
        Destroy(plateObj);
    }
    
}
