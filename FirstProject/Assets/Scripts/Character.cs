using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represent either an NPC or a human Player.
public abstract class Character : HealthEntity
{
    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpForce;

    protected int timeUntilJump = 0;
    public Gender gender;

    public int conventionalAttractiveness;
    // Ranges from 0 to 10.
    // Used by NPCs to calculate sexual attaction.

    public Vehicle vehicle;
    public float healthRegen;
    public Inventory inventory;
    private int stunTime = 0;

    public Character() {
        this.inventory = new Inventory();
        this.inventory.owner = this;
    }

    public void Start() {
        InvokeRepeating("RegenerateHealth", 0f, 1f);

        this.rigidbody.maxAngularVelocity = 100;
        this.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void Update() {
        if (this.isDead) {
            return;
        }

        if (this.stunTime > 0) {
            stunTime--;
            return;
        }

        this.frameUpdate();

        // Drive Vehicle
        if (this.vehicle != null) {
            this.rigidbody.velocity = this.vehicle.rigidbody.velocity;
            this.rigidbody.position = this.vehicle.rigidbody.position;
            this.vehicle.driveVehicle();  // TODO this won't work with NPCs
        }
    }

    public abstract void frameUpdate();

    public void RegenerateHealth() {
        if (this.isDead) {
            return;
        }

        if (this.currentHealth < this.maxHealth - this.healthRegen) {
            this.currentHealth += this.healthRegen;
        } else {
            this.fillHealth();
        }
    }

    public void MoveWithHeading(Vector3 heading) {
        float horizontalSpeed = Mathf.Sqrt(
            Mathf.Pow(this.rigidbody.velocity.x, 2) +
            Mathf.Pow(this.rigidbody.velocity.z, 2)
        );

        if (horizontalSpeed < this.topSpeed) {
            heading.y = 0;
            heading.Normalize();
            rigidbody.AddForce(
                heading*acceleration*(this.topSpeed - horizontalSpeed)/this.topSpeed,
                ForceMode.Impulse
            );
        }
    }

    public void JumpIfGrounded() {
        // TODO use code to find players feet
        Vector3 playersFeet = this.rigidbody.position;
        bool isGrounded = Physics.Raycast(playersFeet, Vector3.down, 1.5f);

        if (isGrounded) {
            rigidbody.AddForce(
                new Vector3(0, this.jumpForce, 0),
                ForceMode.Impulse
            );
        }
    }

    public void EnterVehicle(Vehicle vehicle) {
        vehicle.driver = this;
        this.vehicle = vehicle;
        this.rigidbody.detectCollisions = false;
    }

    public void ExitVehicle() {
        this.rigidbody.velocity = this.vehicle.rigidbody.velocity;
        this.rigidbody.position = this.vehicle.transform.position + this.vehicle.transform.right * 3;
        this.vehicle.driver = null;
        this.vehicle = null;
        this.rigidbody.detectCollisions = true;
    }

    public override void doOtherDamageEffects(Collision collision, float damageAmount) {
        this.stunTime = System.Convert.ToInt32(damageAmount * 3);
    }

    public void AimInDirection(Vector3 direction, float offset) {
        this.rigidbody.angularVelocity = Vector3.zero;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + offset;

        // TODO use torques dont just set rotation.
        this.rigidbody.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    public override float getBaseDamage() {
        return this.rigidbody.angularVelocity.magnitude;
    }

    public override void setDead() {
        base.setDead();
        this.ChangeToColor(Color.gray);
        this.inventory.UnequipItem();
    }

    public void ChangeToColor(Color color) {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) {
            renderer.material.color = color;
        }
    }
}

public enum Gender {
    Male,
    Female,
    Neither,
    Both
}
