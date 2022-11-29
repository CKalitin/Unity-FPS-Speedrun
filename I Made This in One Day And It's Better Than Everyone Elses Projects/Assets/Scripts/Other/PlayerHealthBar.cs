using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [SerializeField] private Image healthBar;

    private Health playerHealth;

    private void Start() {
        UpdatePlayerHealth();
    }

    private void UpdatePlayerHealth() { playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); }

    private void Update() {
        healthBar.fillAmount = playerHealth.CurrentHealth / playerHealth.MaxHealth;
    }
}
