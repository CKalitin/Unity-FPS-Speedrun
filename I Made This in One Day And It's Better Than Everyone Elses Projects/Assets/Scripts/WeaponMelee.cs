using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : MonoBehaviour {
    [Header("Weapon")]
    [SerializeField] private float rateOfAttack;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float dealDamageDelay;
    [Tooltip("Get all health components in sphere of radius attack range from this point.")]
    [SerializeField] private Transform attackPosition;
    [Space]
    [SerializeField] private KeyCode attackKeycode;
    [Space]
    [Tooltip("Uses 'Attack' trigger")]
    [SerializeField] private Animator animator;

    bool isShooting = false;
    bool coolingDownAttack = false;

    private void Update() {
        if (Input.GetKey(attackKeycode)) {
            isShooting = true;
            if (!coolingDownAttack) {
                Attack();
            }
        }

        isShooting = false;
        if (isShooting) return; // Unity stop giving me these warnings
    }

    private void Attack() {
        if (animator) animator.SetTrigger("Attack");
        
        Invoke(nameof(DealDamage), dealDamageDelay);

        coolingDownAttack = true;
        Invoke(nameof(ResetCanShoot), rateOfAttack);
    }

    private void DealDamage() {
        Collider[] colliders = Physics.OverlapSphere(attackPosition.position, attackRange);

        for (int i = 0; i < colliders.Length; i++) {
            Health health = colliders[i].GetComponent<Health>();
            if (health) health.ChangeHealth(-Mathf.Abs(attackDamage));
        }
    }

    private void ResetCanShoot() {
        coolingDownAttack = false;
    }
}
