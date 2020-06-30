using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public PlayerMovement playerScript;
    public Text weaponText;
    public Text vehicleText;

    void Update()
    {
        this.weaponText.text = this.playerScript.currentWeapon().getName();
        if (this.playerScript.vehicle != null) {
            this.vehicleText.text = this.playerScript.vehicle.vehicleText();
        }
    }
}
