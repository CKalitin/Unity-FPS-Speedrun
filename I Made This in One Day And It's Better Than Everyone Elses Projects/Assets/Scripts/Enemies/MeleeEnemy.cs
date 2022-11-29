using UnityEngine;

public class MeleeEnemy : MonoBehaviour {
    [Header("Attacking")]
    [SerializeField] private float attackDistance = 1.5f;

    [Header("Other")]
    [SerializeField] private EnemyPathfinding enemyPathfinding;
    [SerializeField] private WeaponMelee meleeWeapon;
    
    private void Update() {
        if (Vector3.Distance(transform.position, enemyPathfinding.PlayerTransform.position) <= attackDistance) {
            meleeWeapon.Attack();
        }
    }
}
