using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWeaponDisplay : MonoBehaviour {
    [SerializeField] private WeaponDisplay[] weaponDisplays;

    PlayerWeaponController pwc;

    [Serializable]
    public struct WeaponDisplay {
        public GameObject unavailable;
        public GameObject available;
        public GameObject equipped;
        [Space]
        public TextMeshProUGUI[] ammoTexts;
    }

    private void Awake() {
        pwc = FindObjectOfType<PlayerWeaponController>();
    }

    private void Update() {
        SetWeaponDisplays();
    }

    private void SetWeaponDisplays() {
        for (int i = 0; i < pwc.Weapons.Length; i++) {
            if (i == pwc.CurrentWeaponIndex) {
                SetWeaponDisplay(i, false, false, true);
            } else if (pwc.Weapons[i].available) {
                SetWeaponDisplay(i, false, true, false);
            } else {
                SetWeaponDisplay(i, true, false, false);
            }
        }
    }

    private void SetWeaponDisplay(int _index, bool _unavailable, bool _available, bool _equipped) {
        weaponDisplays[_index].unavailable.SetActive(_unavailable);
        weaponDisplays[_index].available.SetActive(_available);
        weaponDisplays[_index].equipped.SetActive(_equipped);
        SetAmmoTexts(_index, _unavailable, _available, _equipped);
    }

    private void SetAmmoTexts(int _index, bool _unavailable, bool _available, bool equipped) {
        if (!pwc.Weapons[_index].weapon.GetComponent<WeaponRanged>()) return;
        
        if (equipped) {
            SetAmmoText(_index, weaponDisplays[_index].equipped.GetComponent<Image>().color.a);
        } else if (_available) {
            SetAmmoText(_index, weaponDisplays[_index].available.GetComponent<Image>().color.a);
        } else {
            SetAmmoText(_index, weaponDisplays[_index].unavailable.GetComponent<Image>().color.a);
        }
    }

    private void SetAmmoText(int _index, float _alpha) {
        for (int i = 0; i < weaponDisplays[_index].ammoTexts.Length; i++) {
            weaponDisplays[_index].ammoTexts[i].color = new Color(weaponDisplays[_index].ammoTexts[i].color.r, weaponDisplays[_index].ammoTexts[i].color.g, weaponDisplays[_index].ammoTexts[i].color.b, _alpha);
            weaponDisplays[_index].ammoTexts[i].text = pwc.Weapons[_index].weapon.GetComponent<WeaponRanged>().AmmoAmount.ToString() + "/" + pwc.Weapons[_index].weapon.GetComponent<WeaponRanged>().MaxAmmo.ToString();
        }
    }
}
