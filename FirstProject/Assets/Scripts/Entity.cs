using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public static float IMPULSE_DAMAGE_MULTIPLIER = 1f;
    public static float DESPAWN_TIME = 15f;

    protected float maxHealth;
    protected float currentHealth;
    private float recentImpulseMagnitude;
    private float minImpulseForDamage;
    private float impulseRecovery;

    public bool isDead;

    public Entity() {
        this.maxHealth = 100;
        this.fillHealth();
        this.impulseRecovery = 0.1f;  // TODO find a better way
        this.recentImpulseMagnitude = 0;
        this.minImpulseForDamage = 10;

        // TODO add more damage for high velocity but low impulse things (like bullets)
    }

    public void OnCollisionEnter(Collision collision) {
        float healthLoss = 0;

        this.recentImpulseMagnitude += collision.impulse.magnitude;

        if (this.recentImpulseMagnitude > this.minImpulseForDamage) {
            // TODO instant death bug, probably to do with qucik accelleration in opposite directions within
            // like 1 frame.
            float impulseDamage = this.recentImpulseMagnitude - this.minImpulseForDamage;
            this.recentImpulseMagnitude -= impulseDamage;
            healthLoss = Mathf.Pow(impulseDamage, 2) * Entity.IMPULSE_DAMAGE_MULTIPLIER;
            this.currentHealth -= healthLoss;
        }

        if (this.currentHealth < 0) {
            this.isDead = true;
            this.currentHealth = 0;
        }

        this.doOtherDamageEffects(collision, healthLoss);
    }

    public void LateUpdate() {
        if (this.recentImpulseMagnitude > this.impulseRecovery) {
            this.recentImpulseMagnitude -= this.impulseRecovery;
        } else {
            this.recentImpulseMagnitude = 0;
        }
    }

    public float remainingHealthPortion() {
        return this.currentHealth/this.maxHealth;
    }

    protected void fillHealth() {
        this.currentHealth = this.maxHealth;
    }

    public void updateMaxHealth(float maxHealth) {
        this.maxHealth = maxHealth;
        this.fillHealth();
    }

    public abstract void doOtherDamageEffects(Collision collision, float damageAmount);  // Use for stunning and friendliness
}
