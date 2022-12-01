using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessGameController : MonoBehaviour {
    [Header("Game")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PickupSpawner pickupSpawner;
    [Space]
    [SerializeField] private GameObject[] deactivateOnDeath;
    private Health playerHealth;

    bool playerDead = false;

    private void Awake() {
        Time.timeScale = 0f;
    }

    private void Start() {
        GetPlayerHealth();
    }

    private void Update() {
        if (playerHealth.CurrentHealth <= 0 && !playerDead) {
            for (int i = 0; i < deactivateOnDeath.Length; i++) {
                deactivateOnDeath[i].SetActive(false);
            }
            FindObjectOfType<MainMenuController>().PlayerDeath();
            playerDead = true;
        }
    }

    private void GetPlayerHealth() { playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); }
    
    public void StartGame() {
        FindObjectOfType<MainMenuController>().ActiveGameUI();
        Time.timeScale = 1f;
    }
    
    public void StartSpawning() {
        pickupSpawner.TogglePickupSpawning(true);
        enemySpawner.SpawnFirstWave();
    }
}
