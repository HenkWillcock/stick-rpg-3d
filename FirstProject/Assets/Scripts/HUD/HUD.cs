using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public PlayerMovement playerScript;
    public Text weaponText;

    void Update()
    {
        this.weaponText.text = this.playerScript.currentWeapon().getName();
    }
}
