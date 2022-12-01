using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScoreComponent : MonoBehaviour {
    [Header("Other")]
    [SerializeField] private Health health;
    
    float previousHealth = 0f;
    bool dead = false;

    ScoreController sc;

    private void Awake() {
        sc = FindObjectOfType<ScoreController>();
    }

    private void Update() {
        if (Mathf.Abs(health.CurrentHealth - previousHealth) > 0) {
            sc.Score += Mathf.Abs(health.CurrentHealth - previousHealth) * sc.ScorePerDamageDealt;
        }

        if (health.CurrentHealth <= 0.1 && !dead) {
            sc.Score += sc.ScorePerEnemyKill;
            dead = true;
        }
        
        previousHealth = health.CurrentHealth;
    }
}
