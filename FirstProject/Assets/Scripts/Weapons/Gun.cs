﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
    public Rigidbody bulletPrefab;

    private float bulletVelocity;
    private int reloadTime;

    private int timeUntilLoaded = 0;

    public Gun(
            Rigidbody usersRigidbody,
            string name,
            Rigidbody bulletPrefab,
            float bulletVelocity,
            int reloadTime) :
            base(usersRigidbody, name) 
        {

        this.bulletPrefab = bulletPrefab;
        this.bulletVelocity = bulletVelocity;
        this.reloadTime = reloadTime;
    }

    public override void effect() {
        aimPlayer(0);

        if (this.timeUntilLoaded == 0) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {  // TODO ignore hidden roofs
                Vector3 objectHitPosition = hit.point;
                Vector3 towardsObject = objectHitPosition - this.usersRigidbody.position;
                towardsObject.Normalize();

                Rigidbody bullet;
                bullet = Object.Instantiate(
                    this.bulletPrefab,
                    this.usersRigidbody.position + towardsObject*3,
                    Quaternion.LookRotation(towardsObject)
                );
                bullet.velocity = towardsObject * this.bulletVelocity;
            }

            this.timeUntilLoaded = this.reloadTime;

        } else if (this.timeUntilLoaded > 0)  {
            this.timeUntilLoaded -= 1;
        }
    }

    public override void idleEffect() {
        if (this.timeUntilLoaded > 0)  {
            this.timeUntilLoaded -= 1;
        }
    }
}