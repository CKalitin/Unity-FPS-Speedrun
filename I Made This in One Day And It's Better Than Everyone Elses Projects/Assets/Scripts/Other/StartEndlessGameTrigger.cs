using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndlessGameTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<EndlessGameController>().StartSpawning();
            gameObject.SetActive(false);
        }
    }
}
