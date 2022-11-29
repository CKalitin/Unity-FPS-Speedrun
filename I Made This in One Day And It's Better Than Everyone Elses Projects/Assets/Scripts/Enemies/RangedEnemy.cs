using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour {
    [Header("Attacking")]
    [SerializeField] private float attackDistance = 6f;

    [Header("Other")]
    [SerializeField] private EnemyPathfinding enemyPathfinding;
    [SerializeField] private WeaponRanged rangedWeapon;

    private void Update() {
        if (Physics.Raycast(rangedWeapon.shootPosition.position, transform.forward, rangedWeapon.shotRange)) {
            if (Vector3.Distance(transform.position, enemyPathfinding.playerTransform.position) <= attackDistance) {
                rangedWeapon.Shoot();
            }
        }
    }
}
