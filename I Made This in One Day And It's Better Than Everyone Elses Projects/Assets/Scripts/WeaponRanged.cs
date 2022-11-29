using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRanged : MonoBehaviour {
    [Header("Weapon")]
    [SerializeField] private float rateOfFire;
    [SerializeField] private float shotDamage;
    [SerializeField] private float shotRange;
    [SerializeField] private Transform shootPosition;
    [Space]
    [SerializeField] private KeyCode shootKeycode;
    [Space]
    [Tooltip("Uses 'Attack' trigger")]
    [SerializeField] private Animator animator;

    bool isShooting = false;
    bool coolingDownShot = false;

    private void Update() {
        if (Input.GetKey(shootKeycode)) {
            isShooting = true;
            if (!coolingDownShot) {
                Shoot();
            }
        }
        
        isShooting = false;
        if (isShooting) return; // Unity stop giving me these warnings
    }

    private void Shoot() {
        RaycastHit hit;
        if (Physics.Raycast(shootPosition.position, transform.forward, out hit, shotRange)) {
            if (hit.collider.gameObject.GetComponent<Health>()) {
                hit.collider.gameObject.GetComponent<Health>().ChangeHealth(-Mathf.Abs(shotDamage));
            }
        }

        if (animator) animator.SetTrigger("Attack");

        coolingDownShot = true;
        Invoke(nameof(ResetShotCooldown), rateOfFire);
    }

    private void ResetShotCooldown() {
        coolingDownShot = false;
    }
}
