using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCanvas : MonoBehaviour {
    private void Awake() {
        transform.LookAt(FindObjectOfType<PlayerMovementController>().transform.position);
    }
}
