using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : HealthEntity
{
    public Transform transform;

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
