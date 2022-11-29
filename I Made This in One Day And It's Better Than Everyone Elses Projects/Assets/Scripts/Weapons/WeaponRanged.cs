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
    [Tooltip("Uses 'Attack' trigger in Animation Controller")]
    [SerializeField] private Animator animator;
    
    bool coolingDownShot = false;
    
    public void Shoot() {
        if (!coolingDownShot) {
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
    }

    private void ResetShotCooldown() {
        coolingDownShot = false;
    }
}
