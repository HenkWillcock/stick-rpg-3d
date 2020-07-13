using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public float motorTorque;
    public float topRPM;
    public float brakeTorque;
    public float maxSteeringAngle;

    public int gear = 1;

    public override void driveVehicle()
    {
        // Brakes
        float brakeTorque = System.Convert.ToSingle(Input.GetKey("s")) * this.brakeTorque;

        this.frontLeftWheel.brakeTorque = brakeTorque;
        this.frontRightWheel.brakeTorque = brakeTorque;
        this.rearLeftWheel.brakeTorque = brakeTorque;
        this.rearRightWheel.brakeTorque = brakeTorque;

        // Accelerate
        float acceleration = System.Convert.ToSingle(Input.GetKey("w")) * this.motorTorque * this.gear;
        float leftWheelLoss = 1 - (this.frontLeftWheel.rpm * Time.deltaTime / this.topRPM);
        float rightWheelLoss = 1 - (this.frontRightWheel.rpm * Time.deltaTime / this.topRPM);

        this.frontLeftWheel.motorTorque = acceleration * leftWheelLoss;
        this.frontRightWheel.motorTorque = acceleration * rightWheelLoss;

        // Steering
        float steerLeft = System.Convert.ToSingle(Input.GetKey("a"));
        float steerRight = System.Convert.ToSingle(Input.GetKey("d"));
        float steerAngle = (steerRight - steerLeft) * this.maxSteeringAngle;

        this.frontLeftWheel.steerAngle = steerAngle;
        this.frontRightWheel.steerAngle = steerAngle;

        // Gears
        if (Input.GetKeyUp("up")) {
            this.gear++;
        } else if (Input.GetKeyUp("down")) {
            this.gear--;
        }
    }

    public override string vehicleText() {
        return
                this.name + "\n\n" +
                Mathf.Round(this.rigidbody.velocity.magnitude * 3.6f).ToString() + " km/h\n" +
                "Gear: " + this.gear.ToString();
    }
}
