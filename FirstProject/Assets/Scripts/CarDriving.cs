using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour
{
    private GameController gameController;

    public Rigidbody rigidbody;
    public Transform transform;

    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public float motorTorque;
    public float brakeTorque;
    public float maxSteeringAngle;

    public int gear = 1;

    void Start() {
        this.gameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameController.gameFocus == this.rigidbody) {

            // Brakes
            float brakeTorque = System.Convert.ToSingle(Input.GetKey("s")) * this.brakeTorque;

            this.frontLeftWheel.brakeTorque = brakeTorque;
            this.frontRightWheel.brakeTorque = brakeTorque;
            this.rearLeftWheel.brakeTorque = brakeTorque;
            this.rearRightWheel.brakeTorque = brakeTorque;

            // Accelerate
            float acceleration = System.Convert.ToSingle(Input.GetKey("w")) * this.motorTorque * this.gear;
            Debug.Log(acceleration);


            this.frontLeftWheel.motorTorque = acceleration;
            this.frontRightWheel.motorTorque = acceleration;

            Debug.Log("Motor torque: " + this.frontLeftWheel.motorTorque + ", Brake torque: " + this.frontLeftWheel.brakeTorque);

            // Steering
            float steerLeft = System.Convert.ToSingle(Input.GetKey("a"));
            float steerRight = System.Convert.ToSingle(Input.GetKey("d"));
            float steerAngle = (steerLeft - steerRight) * this.maxSteeringAngle;

            this.frontLeftWheel.steerAngle = steerAngle;
            this.frontRightWheel.steerAngle = steerAngle;

            // Exit Vehicle
            if (Input.GetKeyDown("f")) {
                gameController.player.gameObject.SetActive(true);
                gameController.player.position = this.rigidbody.position + transform.right * -3;
                gameController.gameFocus = gameController.player;
            }
        }
    }
}
