using UnityEngine;

public class MeleeEnemy : MonoBehaviour {
    [Header("Attacking")]
    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private float rotationSpeed = 120f;

    [Header("Other")]
    [SerializeField] private EnemyPathfinding enemyPathfinding;
    [SerializeField] private WeaponMelee meleeWeapon;
    [SerializeField] private Animator animator;

    Vector3 previousPos;
    bool attacking = false;

    private void Update() {
        RotateTowardsPlayer();

        attacking = false;
        if (Vector3.Distance(transform.position, enemyPathfinding.PlayerTransform.position) <= attackDistance) {
            attacking = true;
            meleeWeapon.Attack();
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
        
        animator.SetBool("running", false);
        animator.SetBool("walking", false);
        animator.SetBool("attacking", false);
        animator.SetBool("idle", false);
        
        if (attacking) animator.SetBool("attacking", true);
        else if (speed >= 0.5f) animator.SetBool("running", true);
        else if (speed >= 0.1f) animator.SetBool("walking", true);
        else animator.SetBool("idle", true);
    }
}
