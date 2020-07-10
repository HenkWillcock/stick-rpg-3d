using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Item {
    private float spinSpeed;

    public Spin(
            string name,
            float spinSpeed
        ) : base(name) {

        this.spinSpeed = spinSpeed;
        this.rangeForNPC = 1.5f;
    }

    public override void effect(Character user, Vector3 targetPosition) {
        float spinLoss = 1 - (user.rigidbody.angularVelocity.magnitude / this.spinSpeed);

        user.rigidbody.AddTorque(
            user.rigidbody.transform.up * spinLoss,  // TODO check if can just go straight from character to transform.
            ForceMode.Impulse);
    }
}
