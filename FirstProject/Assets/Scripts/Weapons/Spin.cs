using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Weapon {
    private float spinSpeed;

    public Spin(
            Rigidbody usersRigidbody,
            string name,
            float spinSpeed
        ) : base(usersRigidbody, name) {

        this.spinSpeed = spinSpeed;
    }

    public override void effect() {
        float spinLoss = 1 - (this.usersRigidbody.angularVelocity.magnitude / this.spinSpeed);

        this.usersRigidbody.AddTorque(
            this.usersRigidbody.transform.up * spinLoss,
            ForceMode.Impulse);
    }

    public override void npcBehaviour(Rigidbody target) {
        this.effect();
    }
}
