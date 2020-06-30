using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleDriving : MonoBehaviour
{
    public Transform transform;
    public Rigidbody rigidbody;
    public Rigidbody bulletPrefab;

    private Weapon weapon;
    public PlayerMovement driver;
    // TODO change to Character after adding weapons to Character properly.
    // TODO rename PlayerMovement to just Player or some shit.

    void Start() {
        this.weapon = new Gun(this.driver, "Machine Gun", this.bulletPrefab, 30, 8); 
    }

    void Update()
    {
        this.idleVehicle();
    }

    public abstract void driveVehicle();

    public virtual void idleVehicle() {
        return;
    }

    public abstract string vehicleText();
}
