using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponText : MonoBehaviour
{   
    public Text weaponText;
    public PlayerWeapons playerWeapons;

    void Update()
    {
        this.weaponText.text = this.playerWeapons.currentWeapon().name;
    }
}
