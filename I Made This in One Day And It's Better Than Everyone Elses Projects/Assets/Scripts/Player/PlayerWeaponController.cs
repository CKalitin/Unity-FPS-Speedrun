using System;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {
    #region Variables

    [Header("Weapon Switching")]
    [SerializeField] private int currentWeaponIndex = 0;
    [SerializeField] private WeaponStruct[] weapons;

    float currentMouseScrollDelta;

    [Serializable]
    private struct WeaponStruct {
        public GameObject weapon;
        public bool available;
    }
    #endregion

    private void Start() {
        SetWeapon(0);
    }

    private void Update() {
        ControlWeaponChanging();
        Attack();
    }

    #region Using Weapons

    private void Attack() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (weapons[currentWeaponIndex].weapon.GetComponent<WeaponMelee>())
                weapons[currentWeaponIndex].weapon.GetComponent<WeaponMelee>().Attack();
            else if (weapons[currentWeaponIndex].weapon.GetComponent<WeaponRanged>())
                weapons[currentWeaponIndex].weapon.GetComponent<WeaponRanged>().Shoot();
        }
    }

    #endregion

    #region Switching Weapons

    private void ControlWeaponChanging() {
        currentMouseScrollDelta += Input.mouseScrollDelta.y;

        if (Mathf.Abs(currentMouseScrollDelta) >= 1) {
            if (currentMouseScrollDelta > 0) ChangeWeapon(1);
            if (currentMouseScrollDelta < 0) ChangeWeapon(-1);
            currentMouseScrollDelta = 0;
        }

        for (int i = 0; i < weapons.Length; i++) {
            if (Input.GetKeyDown((KeyCode)(i + 49))) SetWeapon(i);
        }
    }

    // Holy shit Github Copilot wrote most of this
    private void ChangeWeapon(int _input) {
        weapons[currentWeaponIndex].weapon.SetActive(false);

        bool newWeaponSelected = false;
        int iters = 0;
        while (!newWeaponSelected) {
            currentWeaponIndex += _input;

            if (currentWeaponIndex > weapons.Length - 1) currentWeaponIndex = 0;
            if (currentWeaponIndex < 0) currentWeaponIndex = weapons.Length - 1;

            if (weapons[currentWeaponIndex].available) {
                weapons[currentWeaponIndex].weapon.SetActive(true);
                newWeaponSelected = true;
                break;
            }

            iters++;
            if (iters > weapons.Length) {
                Debug.LogError("No weapons available");
                break;
            }
        }
    }

    private void SetWeapon(int _weaponIndex) {
        if (weapons[currentWeaponIndex].available) {
            weapons[currentWeaponIndex].weapon.SetActive(false);
            currentWeaponIndex = _weaponIndex;
            weapons[currentWeaponIndex].weapon.SetActive(true);
        }
    }

    #endregion
}