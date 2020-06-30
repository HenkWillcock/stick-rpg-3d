using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Player playerScript;
    public Text weaponText;
    public Text vehicleText;
    public RectTransform healthBar;
    public RectTransform staminaBar;
    public RectTransform hungerBar;

    void Update()
    {
        this.healthBar.transform.localScale = new Vector3(playerScript.remainingHealthPortion(), 1, 1);

        this.weaponText.text = this.playerScript.currentWeapon().getName();

        if (this.playerScript.vehicle != null) {
            this.vehicleText.text = this.playerScript.vehicle.vehicleText();
        } else {
            this.vehicleText.text = "";
        }
    }
}
