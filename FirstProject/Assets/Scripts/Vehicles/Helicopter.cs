using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : Vehicle
{
    public Transform blades;

    private float rotorSpeed;

    public override void driveVehicle()
    {
        // Change Rotor Speed
        float increaseRotorSpeed = System.Convert.ToSingle(Input.GetKey("up"));
        float decreaseRotorSpeed = System.Convert.ToSingle(Input.GetKey("down"));
        this.rotorSpeed += increaseRotorSpeed - decreaseRotorSpeed;

        // Cyclic controls
        float leftCyclic = System.Convert.ToSingle(Input.GetKey("q"));
        float rightCyclic = System.Convert.ToSingle(Input.GetKey("e"));
        float leftRightCyclic = leftCyclic - rightCyclic;

        this.rigidbody.AddTorque(transform.forward * leftRightCyclic * 500);

        float forwardCyclic = System.Convert.ToSingle(Input.GetKey("w"));
        float backwardCyclic = System.Convert.ToSingle(Input.GetKey("s"));
        float forwardBackCyclic = forwardCyclic - backwardCyclic;
        this.rigidbody.AddTorque(transform.right * forwardBackCyclic * 500);

        // Tail Rotor
        float leftTailRotor = System.Convert.ToSingle(Input.GetKey("a"));
        float rightTailRotor = System.Convert.ToSingle(Input.GetKey("d"));
        float leftRightTailRotor = rightTailRotor - leftTailRotor;
        this.rigidbody.AddTorque(transform.up * leftRightTailRotor * 500);

        // Corrective Cyclic TODO
    }

    public override void idleVehicle() {
        // Spin Rotor
        this.blades.RotateAround(blades.position, blades.up, this.rotorSpeed/10);

        // Add lift
        this.rigidbody.AddForce(transform.up * this.rotorSpeed, ForceMode.Impulse);

        if (this.driver == null && this.rotorSpeed > 0) {
            this.rotorSpeed--;
        }
    }

    public override string vehicleText()
    {
        return "Rotor Speed: " + Mathf.Round(this.rotorSpeed) + "\n\nUp/Down Arrow - Rotor Speed\nW/S - Pitch,  Q/E - Roll\nA/D - Spin";
    }
}
