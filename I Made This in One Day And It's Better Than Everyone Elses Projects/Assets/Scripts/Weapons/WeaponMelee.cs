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
    [Tooltip("Uses 'Attack' trigger")]
    [SerializeField] private Animator animator;
    [SerializeField] private Health selfHealth;
    
    bool coolingDownAttack = false;
    
    public void Attack() {
        if (!coolingDownAttack) {
            coolingDownAttack = true;
            
            if (animator) animator.SetTrigger("Attack");
            
            Invoke(nameof(DealDamage), dealDamageDelay);
            Invoke(nameof(ResetCanShoot), rateOfAttack);
        }
    }

    private void DealDamage() {
        Collider[] colliders = Physics.OverlapSphere(attackPosition.position, attackRange);

        for (int i = 0; i < colliders.Length; i++) {
            Health health = colliders[i].GetComponent<Health>();
            if (health & health != selfHealth) health.ChangeHealth(-Mathf.Abs(attackDamage));
        }
    }

    private void ResetCanShoot() {
        coolingDownAttack = false;
    }
}
