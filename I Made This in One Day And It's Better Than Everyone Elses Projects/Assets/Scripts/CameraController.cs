using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform targetCameraPosition;
    
    private void Update() {
        transform.rotation = targetCameraPosition.rotation;
        transform.position = targetCameraPosition.position;
    }
}