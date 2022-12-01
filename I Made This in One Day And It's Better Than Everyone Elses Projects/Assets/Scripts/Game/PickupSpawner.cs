using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {
    [SerializeField] private Pickup[] pickups;
    [SerializeField] private bool findAllPickupsAtAwake = true;
    [Space]
    [SerializeField] private float pickupRespawnRate = 40f;

    Coroutine spawnPickupsCoroutine;

    private void Awake() {
        if (findAllPickupsAtAwake) {
            pickups = FindObjectsOfType<Pickup>();
        }
    }

    public void TogglePickupSpawning(bool _toggle) {
        if (_toggle) {
            spawnPickupsCoroutine = StartCoroutine(SpawnPickupsLoop());
        } else {
            StopCoroutine(spawnPickupsCoroutine);
        }  
    }

    private IEnumerator SpawnPickupsLoop() {
        while (true) {
            yield return new WaitForSeconds(pickupRespawnRate);
            RespawnUsedPickups();
        }
    }

    private void RespawnUsedPickups() {
        for (int i = 0; i < pickups.Length; i++) {
            if (pickups[i].gameObject.activeSelf == false)
                pickups[i].gameObject.SetActive(true);
        }
    }
}
