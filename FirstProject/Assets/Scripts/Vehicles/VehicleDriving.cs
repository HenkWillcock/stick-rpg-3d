using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleDriving : MonoBehaviour
{
    public Transform transform;
    public Rigidbody rigidbody;
    public Rigidbody bulletPrefab;

    private GameController gameController;
    private Weapon weapon;
    public PlayerMovement driver;  // TODO populate when entered.  
    // TODO change to Character after adding weapons to Character properly.
    // TODO rename PlayerMovement to just Player or some shit.

    void Start() {
        this.gameController = GameObject.FindObjectOfType<GameController>();
        this.weapon = new Gun(this.driver, "Machine Gun", this.bulletPrefab, 30, 8); 
    }

    void Update()
    {
        if (this.isGameFocus()) {
            if (Input.GetKeyUp("return") && this.gameController.CanSwitchFocus()) {
                // Exit Vehicle
                gameController.player.rigidbody.position = this.rigidbody.position + transform.right * -3;
                gameController.SwitchFocus(gameController.player);
                gameController.player.vehicle = null;
            } else {
                // Use Weapon.
                if (Input.GetMouseButton(0)) {
                    this.driver.currentWeapon().effect();

                } else {
                    this.driver.currentWeapon().idleEffect();
                }
                this.driveVehicle();
            }
        }
        this.idleVehicle();
    }

    public abstract void driveVehicle();

    public virtual void idleVehicle() {
        return;
    }

    public bool isGameFocus() {
        return this.gameController.gameFocus == this.rigidbody;
    }

    public abstract string vehicleText();
}
