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
    [SerializeField] private int ammoAmount = 1000;

    [Header("Audio")]
    [SerializeField] private SoundEffect shotSoundEffect;

    [Header("Other")]
    [SerializeField] private Damager damager;
    [Tooltip("Uses 'Attack' trigger in Animation Controller")]
    [SerializeField] private Animator animator;
    
    bool coolingDownShot = false;

    public float ShotRange { get => shotRange; set => shotRange = value; }
    public Transform ShootPosition { get => shootPosition; set => shootPosition = value; }
    public RangedWeaponAmmoTypes AmmoType { get => ammoType; set => ammoType = value; }
    public int AmmoAmount { get => ammoAmount; set => ammoAmount = value; }

    public void Shoot() {
        if (!coolingDownShot) {
            RaycastHit hit;
            if (Physics.Raycast(shootPosition.position, shootPosition.forward, out hit, shotRange)) {
                damager.DealDamage(new Collider[1] { hit.collider }, -Mathf.Abs(shotDamage));
            }

            if (animator) animator.SetTrigger("Attack");
            
            AudioController.instance.PlayerSoundEffect(shotSoundEffect, shootPosition.position);
            
            coolingDownShot = true;
            Invoke(nameof(ResetShotCooldown), rateOfFire);
        }
    }

    private void ResetShotCooldown() {
        coolingDownShot = false;
    }

    public void ToggleFocus(bool _focusState) {
        animator.SetBool("Focus", _focusState);
    }
}
