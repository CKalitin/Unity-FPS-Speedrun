using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessGameController : MonoBehaviour {
    [Header("Game")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PickupSpawner pickupSpawner;
    private Health playerHealth;

    private void Awake() {
        Time.timeScale = 0f;
    }

    private void Start() {
        GetPlayerHealth();
    }

    private void Update() {
        if (playerHealth.CurrentHealth <= 0) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
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
