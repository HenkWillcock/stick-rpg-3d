using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{

    public static Shotgun SHOTGUN = new Shotgun("Shotgun", 30, 30, 4);
    public static Shotgun DOUBLE_SHOTGUN = new Shotgun("Double Shotgun", 50, 50, 8);
    public static Shotgun AUTO_SHOTGUN = new Shotgun("Auto Shotgun", 30, 10, 4);

    public int numberOfPellets;

    public Shotgun(
        string name,
        float bulletVelocity,
        int reloadTime,
        int numberOfPellets
    ) : base(name, bulletVelocity, reloadTime) {
        this.numberOfPellets = numberOfPellets;
    }

    public override void shoot(Character user, Vector3 towardsObject) {
        for (int i = 0; i < numberOfPellets; i++) {
            this.createBullet(user, towardsObject);
        }
    }
}
