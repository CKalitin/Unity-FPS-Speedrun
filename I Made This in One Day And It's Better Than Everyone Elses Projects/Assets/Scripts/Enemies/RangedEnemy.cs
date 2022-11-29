using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour {
    [Header("Attacking")]
    [SerializeField] private float attackDistance = 12f;
    [SerializeField] private float rotationSpeed = 120f;

    [Header("Other")]
    [SerializeField] private EnemyPathfinding enemyPathfinding;
    [SerializeField] private WeaponRanged rangedWeapon;
    
    private void Update() {
        RotateTowardsPlayer();
        
        RaycastHit hit;
        Debug.DrawRay(rangedWeapon.shootPosition.position, rangedWeapon.shootPosition.forward, Color.red, 5f);
        if (Physics.Raycast(rangedWeapon.shootPosition.position, transform.forward, out hit, rangedWeapon.shotRange)) {
            if (Vector3.Distance(transform.position, enemyPathfinding.playerTransform.position) <= attackDistance)
                rangedWeapon.Shoot();
        }
        Debug.Log(hit.collider.transform);
    }

    private void LateUpdate() {
        RotateTowardsPlayer();
    }

    private void RotateTowardsPlayer() {
        Vector3 targetDirection = (enemyPathfinding.playerTransform.position - transform.position).normalized;
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        targetRotation.y = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        Quaternion weaponTargetRotation = Quaternion.LookRotation(targetDirection);
        weaponTargetRotation.x = 0;
        weaponTargetRotation.z = 0;
        rangedWeapon.transform.rotation = Quaternion.RotateTowards(rangedWeapon.transform.rotation, weaponTargetRotation, rotationSpeed * Time.deltaTime);

    }
}
