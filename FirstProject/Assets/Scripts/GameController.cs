using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Rigidbody gameFocus;
    public Rigidbody player;

    public CameraMovement camera;

    public Text vehicleText;

    private int timeUntilCanSwitchFocus = 0;
    public VehicleDriving vehicleComponent;

    void Start() {
        this.camera.overheadMode = true;
        this.camera.recalculateCameraPosition(30, 25);
    }

    void Update() {
        if (timeUntilCanSwitchFocus > 0) {
            timeUntilCanSwitchFocus--;
        }
        if (this.vehicleComponent != null) {
            this.vehicleText.text = this.vehicleComponent.vehicleText();
        } else {
            this.vehicleText.text = "";
        }

        // TODO only call on SwitchFocus()
    }

    public void SwitchFocus(Rigidbody newFocus) {
        this.gameFocus = newFocus;
        this.timeUntilCanSwitchFocus = 5;

        this.vehicleComponent = this.gameFocus.GetComponent<VehicleDriving>();

        if (this.vehicleComponent != null) {
            this.camera.overheadMode = false;
            this.camera.recalculateCameraPosition(60, 30);
        } else {
            this.camera.overheadMode = true;
            this.camera.recalculateCameraPosition(30, 25);
        }
    }

    public bool CanSwitchFocus() {
        return this.timeUntilCanSwitchFocus == 0;
    }
}
