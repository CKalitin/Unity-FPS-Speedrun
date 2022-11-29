using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [Space]
    [SerializeField] private bool destroyOnZeroHealth;
    
    private void Awake() {
        currentHealth = maxHealth;
    }
    
    public void ChangeHealth(float _damage) {
        currentHealth += _damage;
        if (currentHealth <= 0) {
            if (destroyOnZeroHealth) {
                Destroy(gameObject);
            }
        }
    }
}
