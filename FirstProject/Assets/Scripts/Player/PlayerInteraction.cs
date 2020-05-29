using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameController gameController;
    public Transform transform;

    public void Start() {
        this.gameController = GameObject.FindObjectOfType<GameController>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Vehicle" && Input.GetKeyDown("e")) {
            this.gameController.gameFocus = other.attachedRigidbody;
            transform.gameObject.SetActive(false);
        }
    }
}
