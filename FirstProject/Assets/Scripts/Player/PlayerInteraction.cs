using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameController gameController;
    public Transform transform;

    public void Start() {
        this.gameController = GameObject.FindObjectOfType<GameController>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (
                other.gameObject.GetComponent<VehicleDriving>() != null && 
                Input.GetKeyUp("return") && 
                this.gameController.CanSwitchFocus()
        ) {
            this.gameController.SwitchFocus(other.attachedRigidbody);
            transform.gameObject.SetActive(false);
        }
    }
}
