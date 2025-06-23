using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    private enum Mode {
        LookAt,
        LookAtInverted,
        CamForward,
        CamForwardInverted
    }

    [SerializeField] private Mode mode = Mode.LookAt;

    private void LateUpdate() {
        Camera cam = Camera.main;

        switch (mode) {
            case Mode.LookAt:
                transform.LookAt(cam.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCam = transform.position - cam.transform.position;
                transform.LookAt(transform.position + dirFromCam);
                break;
            case Mode.CamForward:
                transform.forward = cam.transform.forward;
                break;
            case Mode.CamForwardInverted:
                transform.forward = -cam.transform.forward;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}