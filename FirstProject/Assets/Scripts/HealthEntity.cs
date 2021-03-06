﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthEntity : MovingEntity
{
    public static float IMPULSE_DAMAGE_MULTIPLIER = 1f;
    public static float DESPAWN_TIME = 15f;

    protected float maxHealth;
    protected float currentHealth;
    private float recentImpulseMagnitude;
    private float minImpulseForDamage;
    private float impulseRecovery;

    public bool isDead;

    public HealthEntity() {
        this.maxHealth = 100;
        this.fillHealth();
        this.impulseRecovery = 0.1f;  // TODO find a better way
        this.recentImpulseMagnitude = 0;
        this.minImpulseForDamage = 15;
    }

    public void OnCollisionEnter(Collision collision) {
        float healthLoss = 0;

        Entity collisionEntity = collision.gameObject.GetComponent<Entity>();

        if (collisionEntity != null) {
            healthLoss += collisionEntity.getBaseDamage();
        }

        // TODO weaken impact damage after adding base damage.

        this.recentImpulseMagnitude += collision.impulse.magnitude;

        if (this.recentImpulseMagnitude > this.minImpulseForDamage) {
            // TODO instant death bug, probably to do with quick accelleration in opposite directions within
            // like 1 frame.
            float impulseDamage = this.recentImpulseMagnitude - this.minImpulseForDamage;
            this.recentImpulseMagnitude -= impulseDamage;
            healthLoss += Mathf.Pow(impulseDamage, 2) * HealthEntity.IMPULSE_DAMAGE_MULTIPLIER;
        }

        this.currentHealth -= healthLoss;

        Character attacker = null;

        // Reduce friendliness by damage dealt once sorted.
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null) {
            attacker = bullet.shooter;
        }

        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null) {
            attacker = character;
        }

        Vehicle vehicle = collision.gameObject.GetComponent<Vehicle>();
        if (vehicle != null) {
            attacker = vehicle.driver;
        }

        if (attacker != null) {
            this.DoDamageActions(attacker, healthLoss);

            if (attacker is Player player) {
                player.SetEntityClicked(this);
            }
        }

        if (this.currentHealth < 0) {
            this.setDead();
            this.DoDeathActions(attacker);
        }
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

    public abstract void DoDamageActions(Character attacker, float damageAmount);

    public virtual void DoDeathActions(Character attacker) {}

    public virtual void setDead() {
        this.isDead = true;
        this.currentHealth = 0;
        this.ChangeToColor(Color.gray);
    }
}
