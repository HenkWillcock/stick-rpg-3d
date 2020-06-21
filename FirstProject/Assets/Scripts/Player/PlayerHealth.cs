using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
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

    public void LateUpdate() {
        if (this.recentImpulseMagnitude > this.impulseRecovery) {
            this.recentImpulseMagnitude -= this.impulseRecovery;
        } else {
            this.recentImpulseMagnitude = 0;
        }

        // Health Regen
        // TODO dont run every frame
        if (this.currentHealth < this.maxHealth - this.healthRegen) {
            this.currentHealth += this.healthRegen;
        } else if (this.currentHealth < this.healthRegen) {
            this.currentHealth = this.maxHealth;
        }
    }

    public void OnCollisionEnter(Collision collision) {
        this.recentImpulseMagnitude += collision.impulse.magnitude;
        Debug.Log("BAHAHAHA");

        if (this.recentImpulseMagnitude > this.minImpulseForDamage) {
            float impulseDamage = this.recentImpulseMagnitude - this.minImpulseForDamage;
            this.currentHealth -= Mathf.Pow(impulseDamage, 2) * this.impulseDamageMultiplier;
            this.recentImpulseMagnitude -= impulseDamage;
        }

        if (this.currentHealth < 0) {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerWeapons>().enabled = false;
            this.currentHealth = 0;
        }
    }

    public float remainingHealthProportion() {
        return this.currentHealth/this.maxHealth;
    }
}
