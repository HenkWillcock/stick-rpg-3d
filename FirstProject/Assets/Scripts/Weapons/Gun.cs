using System.Collections;
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
            int reloadTime) :
            base(user, name) 
        {

        this.bulletPrefab = bulletPrefab; //((GameObject)Resources.Load("Objects/Bullet", typeof(GameObject))).GetComponent<Rigidbody>();
        this.bulletVelocity = bulletVelocity;
        this.reloadTime = reloadTime;
    }

    public override void effect() {
        // aimPlayer(0);  TODO figure out good way to give this back to player

        if (this.timeUntilLoaded == 0) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {  // TODO ignore hidden roofs
                Vector3 objectHitPosition = hit.point;
                Vector3 towardsObject = objectHitPosition - this.user.rigidbody.position;
                towardsObject.Normalize();

                Rigidbody bullet;
                bullet = Object.Instantiate(
                    this.bulletPrefab,
                    this.user.rigidbody.position + towardsObject * 3,
                    Quaternion.LookRotation(towardsObject)
                );
                bullet.velocity = towardsObject * this.bulletVelocity;
                // bullet.GetComponent<BulletBehaviour>().shooter = this.user  
                // TODO this.user should be a Character object
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
