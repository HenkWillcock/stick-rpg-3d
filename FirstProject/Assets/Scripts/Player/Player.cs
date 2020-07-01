using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Rigidbody bulletPrefab;  // TODO remove this, have a class which is a list of bullet prefabs.

    private int weaponIndex;

    public Transform transform;  // TODO put this on Character

    public CameraMovement cameraScript;

    private int lastInteracted = 0;

    void Start()
    {
        base.Start();

        this.weapons.Add(new Spin(this, "Spin", 15));
        this.weapons.Add(new Spin(this, "Super Spin", 30));
        this.weapons.Add(new Gun(this, "Pistol", this.bulletPrefab, 30, 30));
        this.weapons.Add(new Gun(this, "Machine Gun", this.bulletPrefab, 30, 8));
        this.weapons.Add(new Gun(this, "Sniper", this.bulletPrefab, 80, 60));
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

        // Use Weapon
        if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {  // TODO ignore hidden roofs
                Vector3 objectHitPosition = hit.point;
                this.currentWeapon().effect(objectHitPosition);
            }
        } else {
            this.AimTowardsMouse(90);
        }

        this.currentWeapon().idleEffect();

        // Select Next Weapon
        if (Input.mouseScrollDelta.y > 0 || Input.GetKeyUp(".")) {
            this.weaponIndex++;
        } else if (Input.mouseScrollDelta.y < 0 || Input.GetKeyUp(",")) {
            this.weaponIndex--;
        }

        // Loop Weapon Index Around
        if (this.weapons.Count > 0) {
            while (this.weaponIndex > this.weapons.Count - 1) {
                this.weaponIndex -= this.weapons.Count;
            }
            while (weaponIndex < 0) {
                this.weaponIndex += this.weapons.Count;
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

    public Weapon currentWeapon() {
        return this.weapons[this.weaponIndex];
    }

    public void AimTowardsMouse(float offset) {
        Vector3 directionToMouse = 
                Input.mousePosition - 
                Camera.main.WorldToScreenPoint(this.rigidbody.position);

        directionToMouse.z = directionToMouse.y;  // Because X and Z are the horizontal coordinates.

        this.AimInDirection(directionToMouse, offset);
    }
}
