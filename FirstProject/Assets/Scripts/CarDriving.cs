using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour
{
    private GameController gameController;

    public Rigidbody rigidbody;
    public Transform transform;

    public float thrust;
    public float steeringPower;
    public bool beingDriven = false;

    void Start() {
        this.gameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameController.gameFocus == this.rigidbody) {

            // Forward/Backward
            if (Input.GetKey("s")) {
                rigidbody.AddForce(-this.transform.right * thrust);
            } else if (Input.GetKey("w")) {
                // Accelerate
                rigidbody.AddForce(this.transform.right * thrust);
            }

            // Steering
            float steerLeft = System.Convert.ToSingle(Input.GetKey("a"));
            float steerRight = System.Convert.ToSingle(Input.GetKey("d"));
            float steering = steerLeft - steerRight;
            rigidbody.AddTorque(transform.forward * steering * steeringPower);

            // Exit Vehicle
            if (Input.GetKeyDown("f")) {
                gameController.player.gameObject.SetActive(true);
                gameController.player.position = this.rigidbody.position + transform.up * -3;
                gameController.gameFocus = gameController.player;
            }
        }
    }
}
