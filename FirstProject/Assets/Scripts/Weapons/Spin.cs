using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Weapon {
    private float spinSpeed;

    public Spin(
            Rigidbody user,
            string name,
            float spinSpeed
        ) : base(user, name) {

        this.spinSpeed = spinSpeed;
    }

    public override void effect() {
        float spinLoss = 1 - (this.user.angularVelocity.magnitude / this.spinSpeed);

        this.user.AddTorque(
            this.user.transform.up * spinLoss,
            ForceMode.Impulse);
    }

    public override void npcBehaviour(Transform target) {
        // TODO handling all the behaviour in here doesn't seem like a good idea
    }
}
