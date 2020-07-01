using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Weapon {
    private float spinSpeed;

    public Spin(
            Character user,
            string name,
            float spinSpeed
        ) : base(user, name) {

        this.spinSpeed = spinSpeed;
        this.rangeForNPC = 1.5f;
    }

    public override void effect() {
        float spinLoss = 1 - (this.user.rigidbody.angularVelocity.magnitude / this.spinSpeed);

        this.user.rigidbody.AddTorque(
            this.user.rigidbody.transform.up * spinLoss,  // TODO check if can just go straight from character to transform.
            ForceMode.Impulse);
    }
}
