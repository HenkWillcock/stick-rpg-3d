using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleDriving : MonoBehaviour
{
    public Transform transform;
    public Rigidbody rigidbody;

    private GameController gameController;

    void Start() {
        this.gameController = GameObject.FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (this.isGameFocus()) {
            if (Input.GetKeyUp("return") && this.gameController.CanSwitchFocus()) {
                // Exit Vehicle
                gameController.player.gameObject.SetActive(true);
                gameController.player.position = this.rigidbody.position + transform.right * -3;
                gameController.SwitchFocus(gameController.player);
            } else {
                this.driveVehicle();
            }
        }
    }

    public abstract void driveVehicle();

    public bool isGameFocus() {
        return this.gameController.gameFocus == this.rigidbody;
    }

    public abstract string vehicleText();
}
