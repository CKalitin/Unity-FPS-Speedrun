using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour {
    [Header("Attacking")]
    [SerializeField] private float attackDistance = 12f;
    [SerializeField] private float rotationSpeed = 120f;

    bool aiming = false;
    Vector3 previousPos;

    [Header("Other")]
    [SerializeField] private EnemyPathfinding enemyPathfinding;
    [SerializeField] private WeaponRanged rangedWeapon;
    [SerializeField] private Animator animator;

    private void Update() {
        RotateTowardsPlayer();

        aiming = false;
        RaycastHit hit;
        if (Vector3.Distance(transform.position, enemyPathfinding.PlayerTransform.position) <= attackDistance) {
            aiming = true;
            if (Physics.Raycast(rangedWeapon.ShootPosition.position, rangedWeapon.ShootPosition.forward, out hit, rangedWeapon.ShotRange) && hit.transform.GetComponent<PlayerMovementController>() && animator.GetBool("running") == false) {
                rangedWeapon.Shoot();
            }
        }

        Animation();
    }

    private void RotateTowardsPlayer() {
        Vector3 targetDirection = (enemyPathfinding.PlayerTransform.position - transform.position).normalized;
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Animation() {
        float speed = (transform.position - previousPos).magnitude / Time.deltaTime;
        previousPos = transform.position;
        
        if (speed >= 1f && Vector3.Distance(transform.position, enemyPathfinding.PlayerTransform.position) >= attackDistance) {
            animator.SetBool("running", true);
            animator.SetBool("walking", false);
            animator.SetBool("aiming", false);
            animator.SetBool("idle", false);
        } else if (speed >= 0.01f) {
            animator.SetBool("running", false);
            animator.SetBool("walking", true);
            animator.SetBool("aiming", aiming);
            animator.SetBool("idle", false);
        } else {
            animator.SetBool("running", false);
            animator.SetBool("walking", false);
            animator.SetBool("aiming", aiming);
            animator.SetBool("idle", true);
        }
    }
}
