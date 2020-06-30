using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
    public Rigidbody bulletPrefab;

    private List<Weapon> weapons;
    private int weaponIndex;
    
    private GameController gameController;
    public Transform transform;  // TODO put this on Character

    public Weapon currentWeapon() {
        return this.weapons[this.weaponIndex];
    }

    void Start()
    {
        rigidbody.maxAngularVelocity = 30;

        this.weapons = new List<Weapon>();

        // TODO add PlayerHealth to Character, as both NPCs and Players should have this
        this.weapons.Add(new Spin(this, "Spin", 15));
        this.weapons.Add(new Gun(this, "Pistol", this.bulletPrefab, 30, 30));
        this.weapons.Add(new Gun(this, "Machine Gun", this.bulletPrefab, 30, 8));
        this.weapons.Add(new Gun(this, "Sniper", this.bulletPrefab, 80, 60));

        this.gameController = GameObject.FindObjectOfType<GameController>();
    }

    public override void subUpdate()
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
            this.currentWeapon().effect();
        } else {
            this.currentWeapon().idleEffect();
            aimPlayer(90);
        }

        // Select Next Weapon
        if (Input.mouseScrollDelta.y > 0) {
            this.weaponIndex++;
        } else if (Input.mouseScrollDelta.y < 0) {
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
    }

    public void OnTriggerStay(Collider other)
    {
        VehicleDriving vehicleScript = other.gameObject.GetComponent<VehicleDriving>();
        if (
                vehicleScript != null && 
                Input.GetKeyUp("return") && 
                this.gameController.CanSwitchFocus()
        ) {
            this.gameController.SwitchFocus(other.attachedRigidbody);
            vehicleScript.driver = this;
            this.vehicle = vehicleScript;
            this.rigidbody.detectCollisions = false;
        }
    }

    protected void aimPlayer(float offset) {
        this.rigidbody.angularVelocity = Vector3.zero;
        float angleToMouse = Helpers.angleFromPositionToMouse(this.rigidbody.position) + offset;
        this.rigidbody.rotation = Quaternion.AngleAxis(angleToMouse, Vector3.up);
    }
}

public class Helpers {
    public static float angleFromPositionToMouse(Vector3 position) {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(position);
        return Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
    }
}
