using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private int currentItemIndex;

    public Transform transform;  // TODO put this on Character

    public CameraMovement cameraScript;

    private int lastInteracted = 0;

    public NPC npcClicked;

    void Start()
    {
        base.Start();

        this.name = "Player";

        this.inventory.Add(new Item("Interact"));

        this.inventory.Add(new Spin("Spin", 15));
        this.inventory.Add(new Spin("Super Spin", 30));

        this.inventory.Add(Gun.PISTOL);
        this.inventory.Add(Gun.MACHINE_PISTOL);
        this.inventory.Add(Gun.ASSAULT_RIFLE);
        this.inventory.Add(Gun.SNIPER);
        this.inventory.Add(Gun.HEAVY_SNIPER);
        this.inventory.Add(Shotgun.SHOTGUN);
        this.inventory.Add(Shotgun.DOUBLE_SHOTGUN);
        this.inventory.Add(Shotgun.AUTO_SHOTGUN);
    }

    public override void frameUpdate()
    {
        // WASD Movement
        float leftRightMovement = 
                System.Convert.ToSingle(Input.GetKey("d")) -
                System.Convert.ToSingle(Input.GetKey("a"));

        float upDownMovement = 
                System.Convert.ToSingle(Input.GetKey("w")) -
                System.Convert.ToSingle(Input.GetKey("s"));

        this.MoveWithHeading(new Vector3(leftRightMovement, 0, upDownMovement));

        // Jumping
        if (Input.GetKey("space") && this.timeUntilJump == 0) {
            this.JumpIfGrounded();
            this.timeUntilJump = 5;
        } else if (this.timeUntilJump > 0) {
            this.timeUntilJump--;
        }

        if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {  // TODO ignore hidden roofs
                if (this.currentItem().getName() == "Interact") {
                    this.Interact(hit.collider.gameObject);
                } else {
                    Vector3 objectHitPosition = hit.point;
                    this.currentItem().effect(this, objectHitPosition);
                }
            }
        } else {
            this.AimTowardsMouse(90);
        }

        this.currentItem().idleEffect();

        // Select Next Item
        if (Input.mouseScrollDelta.y > 0 || Input.GetKeyUp(".")) {
            this.currentItemIndex++;
        } else if (Input.mouseScrollDelta.y < 0 || Input.GetKeyUp(",")) {
            this.currentItemIndex--;
        }

        // Loop Item Index Around
        if (this.inventory.Count > 0) {
            while (this.currentItemIndex > this.inventory.Count - 1) {
                this.currentItemIndex -= this.inventory.Count;
            }
            while (currentItemIndex < 0) {
                this.currentItemIndex += this.inventory.Count;
            }
        }

        // Exit Vehicle
        if (this.vehicle != null && Input.GetKeyUp("return") && this.lastInteracted == 0) {
            this.cameraScript.recalculateCameraPosition(30, 25);
            this.lastInteracted = 5;
            this.ExitVehicle();
        }

        // Reduce Last Interacted
        if (this.lastInteracted > 0) {
            this.lastInteracted--;
        }
    }

    public void Interact(GameObject gameObject) {
        NPC npc = gameObject.GetComponent<NPC>();

        if (npc != null) {
            this.npcClicked = npc;
        } else {
            this.npcClicked = null;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        // Enter Vehicle
        Vehicle vehicle = other.gameObject.GetComponent<Vehicle>();

        if (vehicle != null && Input.GetKeyUp("return") && this.lastInteracted == 0) {
            this.lastInteracted = 5;
            this.EnterVehicle(vehicle);
            this.cameraScript.recalculateCameraPosition(60, 30);
        }
    }

    public Item currentItem() {
        return this.inventory[this.currentItemIndex];
    }

    public void AimTowardsMouse(float offset) {
        Vector3 directionToMouse = 
                Input.mousePosition - 
                Camera.main.WorldToScreenPoint(this.rigidbody.position);

        directionToMouse.z = directionToMouse.y;  // Because X and Z are the horizontal coordinates.

        this.AimInDirection(directionToMouse, offset);
    }
}
