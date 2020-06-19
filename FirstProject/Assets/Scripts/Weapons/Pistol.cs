using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
    public Rigidbody bulletPrefab;
    public float bulletVelocity;

    public Pistol(Rigidbody playerRigidbody, Rigidbody bulletPrefab) : base(playerRigidbody) {
        this.bulletPrefab = bulletPrefab;
    }

    public override void effect() {
        aimPlayer(-90);

        Rigidbody bullet;
        bullet = Instantiate(this.bulletPrefab, this.playerRigidbody.position, this.playerRigidbody.rotation);
        Vector3 towardsMouse = Input.mousePosition - this.playerRigidbody.position;
        towardsMouse.Normalize();
        bullet.velocity = towardsMouse * this.bulletVelocity;
    }
}
