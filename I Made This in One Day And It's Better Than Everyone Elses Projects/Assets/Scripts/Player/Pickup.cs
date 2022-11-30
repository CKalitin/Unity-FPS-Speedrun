using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupTypes {
    Health,
    HandgunAmmo,
    SemiAutoAmmo,
    Weapon
}

public class Pickup : MonoBehaviour {
    [SerializeField] private PickupTypes pickupType;
    [Space]
    [Tooltip("This is used if it is an ammo or health pickup.")]
    [SerializeField] private int amount;
    [Tooltip("This is only used if it is an ammo pickup.")]
    [SerializeField] private RangedWeaponAmmoTypes ammoType;
    [Tooltip("This is only used if it is a weapon pickup.")]
    [SerializeField] private string weaponTag;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            switch (pickupType) {
                case PickupTypes.HandgunAmmo:
                AmmoPickup();
                break;
                case PickupTypes.SemiAutoAmmo:
                AmmoPickup();
                break;
                case PickupTypes.Health:
                HealthPickup();
                break;
                case PickupTypes.Weapon:
                WeaponPickup();
                break;
            }
        }
    }

    private void AmmoPickup() {
        PlayerWeaponController pwc = FindObjectOfType<PlayerWeaponController>();
        WeaponRanged wp = pwc.Weapons[pwc.CurrentWeaponIndex].weapon.GetComponent<WeaponRanged>();

        if (!wp) return; // If current weapon is ranged weapon
        if (!pwc.Weapons[pwc.CurrentWeaponIndex].available) return; // If current weapon is available
        if (wp.AmmoType != ammoType) return; // If current weapon takes proper ammo type
        if (wp.AmmoAmount >= wp.MaxAmmo) return; // If current weapon is full

        wp.AmmoAmount = Mathf.Clamp(wp.AmmoAmount + amount, 0, wp.MaxAmmo);
        
        Destroy(gameObject);
    }

    private void HealthPickup() {
        Health ph = FindObjectOfType<PlayerMovementController>().GetComponent<Health>();

        if (ph.CurrentHealth < ph.MaxHealth) {
            ph.ChangeHealth(Mathf.Abs(amount));
            Destroy(gameObject);
        }
    }

    private void WeaponPickup() {
        PlayerWeaponController pwc = FindObjectOfType<PlayerWeaponController>();

        for (int i = 0; i < pwc.Weapons.Length; i++) {
            if (pwc.Weapons[i].weaponTag == weaponTag && !pwc.Weapons[i].available) {
                pwc.Weapons[i].available = true;
                pwc.SetWeapon(i);
                Destroy(gameObject);
            }
        }
    }
}
