using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represent either an NPC or a human Player.
public abstract class Character : MonoBehaviour
{
    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpForce;

    public Rigidbody rigidbody;

    protected int timeUntilJump = 0;
    public Gender gender;

    public int conventionalAttractiveness;
    // Ranges from 0 to 10.
    // Used by NPCs to calculate sexual attaction.

    public VehicleDriving vehicle;

    public float maxHealth;
    public float impulseDamageMultiplier;
    public float minImpulseForDamage;
    public float impulseRecovery;
    public float healthRegen;

    private float currentHealth;
    private float recentImpulseMagnitude;

    public void Start() {
        this.currentHealth = this.maxHealth;
        this.recentImpulseMagnitude = 0;
    }

    void Update() {
        // Drive Vehicle
        if (this.vehicle != null) {
            this.rigidbody.velocity = this.vehicle.rigidbody.velocity;
            this.rigidbody.position = this.vehicle.rigidbody.position;
            this.vehicle.driveVehicle();  // TODO this won't work with NPCs
        }
        this.subUpdate();  // TODO replace with base.Update();
    }

    public void LateUpdate() {
        if (this.recentImpulseMagnitude > this.impulseRecovery) {
            this.recentImpulseMagnitude -= this.impulseRecovery;
        } else {
            this.recentImpulseMagnitude = 0;
        }

        // Health Regen
        // TODO sort out
        if (this.currentHealth < this.maxHealth - this.healthRegen) {
            this.currentHealth += this.healthRegen * Time.deltaTime;
        } else if (this.currentHealth < this.healthRegen) {
            this.currentHealth = this.maxHealth;
        }
    }

    public abstract void subUpdate();

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

    public void EnterVehicle(VehicleDriving vehicle) {
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

    public void OnCollisionEnter(Collision collision) {
        if (this.tag != "Player") {
            Debug.Log(this.currentHealth);
        }

        this.recentImpulseMagnitude += collision.impulse.magnitude;

        if (this.recentImpulseMagnitude > this.minImpulseForDamage) {
            // TODO instant death bug, probably to do with qucik accelleration in opposite directions within
            // like 1 frame.
            float impulseDamage = this.recentImpulseMagnitude - this.minImpulseForDamage;
            this.currentHealth -= Mathf.Pow(impulseDamage, 2) * this.impulseDamageMultiplier;
            this.recentImpulseMagnitude -= impulseDamage;
        }

        if (this.currentHealth < 0) {
            // TODO why don't NPCs die?
            this.enabled = false;
            this.currentHealth = 0;
        }
    }

    public float remainingHealthPortion() {
        return this.currentHealth/this.maxHealth;
    }
}

public enum Gender {
    Male,
    Female,
    Neither,
    Both
}
