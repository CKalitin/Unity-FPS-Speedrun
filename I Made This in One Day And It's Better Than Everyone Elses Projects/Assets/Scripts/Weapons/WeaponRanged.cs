using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangedWeaponAmmoTypes {
    Handgun,
    SemiAuto
}

public class WeaponRanged : MonoBehaviour {
    [Header("Weapon")]
    [SerializeField] private float rateOfFire;
    [SerializeField] private float shotDamage;
    [SerializeField] private float shotRange;
    [SerializeField] private Transform shootPosition;

    [Header("Ammo")]
    [Tooltip("This is used by the Player Pickup Collector")]
    [SerializeField] private RangedWeaponAmmoTypes ammoType;
    [SerializeField] private int ammoAmount = 100;
    [SerializeField] private int maxAmmo = 100;
    [SerializeField] private bool useAmmo = false;

    [Header("Polish")]
    [SerializeField] private SoundEffect shotSoundEffect;
    [Space]
    [SerializeField] private GameObject shotVFX;
    [SerializeField] private bool useShotVFX = true;
    [Space]
    [Tooltip("This is instantiated at the location where the raycast hit.")]
    [SerializeField] private GameObject hitObject;
    [SerializeField] private bool useHitObject = false;
    private static float hitObjectDestroyDelay = 0.2f;

    [Header("Other")]
    [SerializeField] private Damager damager;
    [Tooltip("Uses 'Attack' trigger in Animation Controller")]
    [SerializeField] private Animator animator;
    
    bool coolingDownShot = false;

    public float ShotRange { get => shotRange; set => shotRange = value; }
    public Transform ShootPosition { get => shootPosition; set => shootPosition = value; }
    public RangedWeaponAmmoTypes AmmoType { get => ammoType; set => ammoType = value; }
    public int AmmoAmount { get => ammoAmount; set => ammoAmount = value; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }

    public void Shoot() {
        if (coolingDownShot) return;
        if (useAmmo && ammoAmount <= 0) return;

        RaycastHit hit;
        if (Physics.Raycast(shootPosition.position, shootPosition.forward, out hit, shotRange)) {
            damager.DealDamage(new Collider[1] { hit.collider }, -Mathf.Abs(shotDamage));
            if (useHitObject) {
                GameObject newHitObject = Instantiate(hitObject, hit.point, Quaternion.LookRotation(transform.position - hit.point));
                Destroy(newHitObject, hitObjectDestroyDelay);
            }
        }
        
        ammoAmount -= 1;

        if (animator) animator.SetTrigger("Attack");
        if (useShotVFX) Instantiate(shotVFX, shootPosition.position, ShootPosition.rotation);
        AudioController.instance.PlayerSoundEffect(shotSoundEffect, shootPosition.position);
        
        coolingDownShot = true;
        Invoke(nameof(ResetShotCooldown), rateOfFire);
    }

    private void ResetShotCooldown() {
        coolingDownShot = false;
    }

    public void ToggleFocus(bool _focusState) {
        animator.SetBool("Focus", _focusState);
    }
}
