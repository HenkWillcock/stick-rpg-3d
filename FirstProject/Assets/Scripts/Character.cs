using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represent either an NPC or a human Player.
public abstract class Character : MonoBehaviour
{
    public static float IMPULSE_DAMAGE_MULTIPLIER = 1f;
    public static float DESPAWN_TIME = 15f;

    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpForce;

    public Rigidbody rigidbody;

    protected int timeUntilJump = 0;
    public Gender gender;

    public int conventionalAttractiveness;
    // Ranges from 0 to 10.
    // Used by NPCs to calculate sexual attaction.

    public Vehicle vehicle;

    public float healthRegen;

    public List<Weapon> weapons;  // TODO expand this to be List<Item> inventory, where Item is base class of Weapon

    private int stunTime = 0;

    private bool dead;

    private float maxHealth;
    private float currentHealth;
    private float recentImpulseMagnitude;
    private float minImpulseForDamage;
    private float impulseRecovery;

    public Character() {
        this.weapons = new List<Weapon>();

        this.maxHealth = 100;
        this.impulseRecovery = 0.1f;  // TODO find a better way
        this.currentHealth = this.maxHealth;
        this.recentImpulseMagnitude = 0;

        if (this.jumpForce < 13) {
            this.minImpulseForDamage = 10;
        } else {
            // Shouldn't be able to take fall damage from a jump.
            this.minImpulseForDamage = this.jumpForce - 3;
        }
    }

    public void Start() {
        InvokeRepeating("RegenerateHealth", 0f, 1f);
    }

    public void Update() {
        if (this.dead) {
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

    public void LateUpdate() {
        if (this.recentImpulseMagnitude > this.impulseRecovery) {
            this.recentImpulseMagnitude -= this.impulseRecovery;
        } else {
            this.recentImpulseMagnitude = 0;
        }
    }

    public void RegenerateHealth() {
        if (this.currentHealth < this.maxHealth - this.healthRegen) {
            this.currentHealth += this.healthRegen;
        } else {
            this.currentHealth = this.maxHealth;
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

    public void OnCollisionEnter(Collision collision) {
        this.recentImpulseMagnitude += collision.impulse.magnitude;

        if (this.recentImpulseMagnitude > this.minImpulseForDamage) {
            // TODO instant death bug, probably to do with qucik accelleration in opposite directions within
            // like 1 frame.
            float impulseDamage = this.recentImpulseMagnitude - this.minImpulseForDamage;
            this.recentImpulseMagnitude -= impulseDamage;
            float healthLoss = Mathf.Pow(impulseDamage, 2) * Character.IMPULSE_DAMAGE_MULTIPLIER;
            this.currentHealth -= healthLoss;
            this.stunTime = System.Convert.ToInt32(healthLoss * 2);
        }

        if (this.currentHealth < 0) {
            this.dead = true;
            this.currentHealth = 0;
        }
    }

    public void AimInDirection(Vector3 direction, float offset) {
        this.rigidbody.angularVelocity = Vector3.zero;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + offset;
        this.rigidbody.rotation = Quaternion.AngleAxis(angle, Vector3.up);
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
