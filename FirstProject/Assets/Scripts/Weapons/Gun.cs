﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
    public Rigidbody bulletPrefab;

    private float bulletVelocity;
    private int reloadTime;

    private int timeUntilLoaded = 0;

    public Gun(
        Character user,
        string name,
        Rigidbody bulletPrefab,
        float bulletVelocity,
        int reloadTime
    ) :
        base(user, name) 
        {

        this.bulletPrefab = bulletPrefab;
        this.bulletVelocity = bulletVelocity;
        this.reloadTime = reloadTime;
        this.rangeForNPC = 15f;
    }

    public override void effect(Vector3 targetPosition) {
            Vector3 towardsObject = targetPosition - this.user.rigidbody.position;
            this.user.AimInDirection(towardsObject, 0);

            if (this.timeUntilLoaded == 0) {
                towardsObject.Normalize();

                Rigidbody bullet = Object.Instantiate(
                    this.bulletPrefab,
                    this.user.rigidbody.position + towardsObject * 3,
                    Quaternion.LookRotation(towardsObject)
                );
                bullet.velocity = towardsObject * this.bulletVelocity;
                bullet.GetComponent<BulletBehaviour>().shooter = this.user;

                this.timeUntilLoaded = this.reloadTime;
            }
    }

    public override void idleEffect() {
        if (this.timeUntilLoaded > 0)  {
            this.timeUntilLoaded -= 1;
        }
    }
}
