using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRanged : MonoBehaviour {
    [Header("Weapon")]
    [SerializeField] private float rateOfFire;
    [SerializeField] private float shotDamage;
    [SerializeField] public float shotRange;
    [SerializeField] public Transform shootPosition;
    [Space]
    [SerializeField] private Damager damager;
    [Tooltip("Uses 'Attack' trigger in Animation Controller")]
    [SerializeField] private Animator animator;
    
    bool coolingDownShot = false;
    
    public void Shoot() {
        if (!coolingDownShot) {
            RaycastHit hit;
            if (Physics.Raycast(shootPosition.position, shootPosition.forward, out hit, shotRange)) {
                damager.DealDamage(new Collider[1] { hit.collider }, -Mathf.Abs(shotDamage));
            }

            if (animator) animator.SetTrigger("Attack");

            coolingDownShot = true;
            Invoke(nameof(ResetShotCooldown), rateOfFire);
        }
    }

    private void ResetShotCooldown() {
        coolingDownShot = false;
    }
}
