using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Rigidbody gameFocus;
    public Rigidbody player;

    public CameraMovement cameraScript;

    public Text vehicleText;

    private int timeUntilCanSwitchFocus = 0;
    public VehicleDriving vehicleComponent;

    void Start() {
        // while(this.cameraScript == null) {
        //     int x = 5;
        // }
        // this.cameraScript.overheadMode = true;
        // this.cameraScript.recalculateCameraPosition(30, 25);
        StartCoroutine(this.LateStart(0.1f));
    }

    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        this.SwitchFocus(this.player);
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
            this.cameraScript.overheadMode = false;
            this.cameraScript.recalculateCameraPosition(60, 30);
        } else {
            this.cameraScript.overheadMode = true;
            this.cameraScript.recalculateCameraPosition(30, 25);
        }
    }

    public bool CanSwitchFocus() {
        return this.timeUntilCanSwitchFocus == 0;
    }
}
