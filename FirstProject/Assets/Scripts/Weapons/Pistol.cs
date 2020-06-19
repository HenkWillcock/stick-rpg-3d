using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
    public Rigidbody bulletPrefab;
    public float bulletVelocity;
    public int reloadTime;

    private int timeUntilLoaded = 0;

    public Pistol(Rigidbody playerRigidbody, Rigidbody bulletPrefab) : base(playerRigidbody) {
        this.bulletPrefab = bulletPrefab;
    }

    public override void effect() {
        aimPlayer(-90);
        
        if (this.timeUntilLoaded == 0) {
            Rigidbody bullet;
            bullet = Instantiate(this.bulletPrefab, this.playerRigidbody.position, this.playerRigidbody.rotation);
            Vector3 towardsMouse = Input.mousePosition - Camera.main.WorldToScreenPoint(this.playerRigidbody.position);
            towardsMouse.Normalize();
            bullet.position += towardsMouse;
            bullet.velocity = towardsMouse * this.bulletVelocity;
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
