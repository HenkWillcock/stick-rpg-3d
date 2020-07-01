using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : Entity
{
    // TODO create a class Entity which Vehicle and Character both extend.
    // Will give health and anything else they share.

    public Transform transform;
    public Rigidbody rigidbody;

    public Character driver;

    void Update()
    {
        this.idleVehicle();
    }

    public abstract void driveVehicle();

    public virtual void idleVehicle() {
        return;
    }

    public abstract string vehicleText();

    public override void doOtherDamageEffects(Collision collision, float damageAmount) {}
}
