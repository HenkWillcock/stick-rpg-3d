using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Weapon {
    public float spinSpeed;

    public Spin(Rigidbody playerRigidbody, float spinSpeed) : base(playerRigidbody) {
        this.spinSpeed = spinSpeed;
    }

    public override void effect() {
        this.playerRigidbody.angularVelocity = new Vector3(0, 0, this.spinSpeed);
    }
}
