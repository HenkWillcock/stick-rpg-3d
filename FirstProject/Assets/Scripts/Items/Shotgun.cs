using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{

    public static Shotgun SHOTGUN = new Shotgun("Shotgun", 400, 0.2f, 30, 30, 4);
    public static Shotgun DOUBLE_SHOTGUN = new Shotgun("Double Shotgun", 2000, 0.05f, 50, 50, 8);
    public static Shotgun AUTO_SHOTGUN = new Shotgun("Auto Shotgun", 3000, 0.05f, 30, 10, 4);

    public int numberOfPellets;

    public Shotgun(
        string name,
        int moneyValue,
        float chanceNPCHas,
        float bulletVelocity,
        int reloadTime,
        int numberOfPellets
    ) : base(name, moneyValue, chanceNPCHas, bulletVelocity, reloadTime) {
        this.numberOfPellets = numberOfPellets;
    }

    public override void shoot(Character user, Vector3 towardsObject) {
        for (int i = 0; i < numberOfPellets; i++) {
            this.createBullet(user, towardsObject);
        }
    }
}
