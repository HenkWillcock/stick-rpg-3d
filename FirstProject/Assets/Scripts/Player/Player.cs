using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Transform transform;  // TODO put this on Character

    public CameraMovement cameraScript;

    private int lastInteracted = 0;

    public NPC npcClicked;

    public HUD hud;

    void Start()
    {
        base.Start();

        this.name = "Player";

        this.inventory.items.Add(new Item("Interact", 0, 0));
        this.inventory.items.Add(Spin.BASIC_SPIN());
        this.inventory.items.Add(Gun.PISTOL());

        this.updateMaxHealth(300);

        this.inventory.money = 1000;
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
                if (this.inventory.currentItem().getName() == "Interact") {
                    this.Interact(hit.collider.gameObject);
                } else {
                    Vector3 objectHitPosition = hit.point;
                    this.inventory.currentItem().effect(this, objectHitPosition);
                }
            }
        } else {
            this.AimTowardsMouse(90);
        }

        this.inventory.currentItem().idleEffect();

        // Select Next Item
        bool scrollUp = Input.mouseScrollDelta.y > 0 || Input.GetKeyUp(".");
        bool scrollDown = Input.mouseScrollDelta.y < 0 || Input.GetKeyUp(",");

        if (scrollUp || scrollDown) {

            if (this.inventory.inventoryHud != null && !this.inventory.inventoryHud.isExpanded) {
                this.inventory.inventoryHud.ExpandFor1Second();
    
            } else if (scrollUp) {
                this.inventory.selectPreviousItem();
                this.inventory.inventoryHud.ExpandFor1Second();

            } else if (scrollDown) {
                this.inventory.selectNextItem();
                this.inventory.inventoryHud.ExpandFor1Second();
            }
        }

        // Expand Inventory
        if (Input.GetKeyUp("tab")) {
            if (this.inventory.inventoryHud != null) {
                this.inventory.inventoryHud.isExpanded = !this.inventory.inventoryHud.isExpanded;
                this.inventory.inventoryHud.UpdateInventorySlots();
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
            this.SetNPCClicked(npc);
        }
        // TODO put back in but not if clicking button
        // else {
        //     this.npcClicked = null;
        // }
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

    public void AimTowardsMouse(float offset) {
        Vector3 directionToMouse = 
                Input.mousePosition - 
                Camera.main.WorldToScreenPoint(this.rigidbody.position);

        directionToMouse.z = directionToMouse.y;  // Because X and Z are the horizontal coordinates.

        this.AimInDirection(directionToMouse, offset);
    }

    public void SetNPCClicked(NPC npc) {
        this.npcClicked = npc;
        this.hud.npcInventory.SetInventory(this.npcClicked.inventory);
    }
}
