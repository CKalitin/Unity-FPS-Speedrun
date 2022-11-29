using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessGameController : MonoBehaviour {
    [Header("Game")]
    private Health playerHealth;

    private void Start() {
        UpdatePlayerHealth();
    }

    private void UpdatePlayerHealth() { playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); }

    private void Update() {
        if (playerHealth.CurrentHealth <= 0) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }
}
