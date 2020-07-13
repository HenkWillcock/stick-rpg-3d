using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Item {
    public static Spin BASIC_SPIN() {return new Spin("Spin", 0, 1.0f, 15);}
    public static Spin SUPER_SPIN() {return new Spin("Super Spin", 500, 0.1f, 30);}

    private float spinSpeed;

    public Spin(
            string name,
            int moneyValue,
            float chanceNPCHas,
            float spinSpeed
        ) : base(name, moneyValue, chanceNPCHas) {

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
