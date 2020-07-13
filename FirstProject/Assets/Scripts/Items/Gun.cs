using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item {
    public static GameObject bulletPrefab = Resources.Load<GameObject>("Objects/Bullet");

    public static Gun PISTOL(){return new Gun("Pistol", 300, 0.5f, 30, 30);}
    public static Gun MACHINE_PISTOL(){return new Gun("Machine Pistol", 800, 0.1f, 30, 8);}
    public static Gun ASSAULT_RIFLE(){return new Gun("Assault Rifle", 1800, 0.1f, 45, 15);}
    public static Gun SNIPER(){return new Gun("Sniper", 2500, 0.1f, 60, 60);}
    public static Gun HEAVY_SNIPER(){return new Gun("Heavy Sniper", 6000, 0.05f, 90, 90);}

    private float bulletVelocity;
    private int reloadTime;

    private int timeUntilLoaded = 0;

    public Gun(
        string name,
        int moneyValue,
        float chanceNPCHas,
        float bulletVelocity,
        int reloadTime
    ) :
        base(name, moneyValue, chanceNPCHas)
    {
        this.bulletVelocity = bulletVelocity;
        this.reloadTime = reloadTime;
        this.rangeForNPC = 15f;
    }

    public override void effect(Character user, Vector3 targetPosition) {
        Vector3 towardsObject = targetPosition - user.rigidbody.position;
        user.AimInDirection(towardsObject, 0);

        if (this.timeUntilLoaded == 0) {
            towardsObject.Normalize();
            this.shoot(user, towardsObject);
            this.timeUntilLoaded = this.reloadTime;
        }
    }

    public virtual void shoot(Character user, Vector3 towardsObject) {
        this.createBullet(user, towardsObject);
    }

    public void createBullet(Character user, Vector3 towardsObject) {
        GameObject bullet = Object.Instantiate(
            Gun.bulletPrefab,
            user.rigidbody.position + towardsObject * 3,
            Quaternion.LookRotation(towardsObject)
        ) as GameObject;

        bullet.GetComponent<Rigidbody>().velocity = towardsObject * this.bulletVelocity;
        bullet.GetComponent<Bullet>().shooter = user;
    }

    public override void idleEffect() {
        if (this.timeUntilLoaded > 0)  {
            this.timeUntilLoaded -= 1;
        }
    }
}
