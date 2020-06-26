using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon {
    protected Rigidbody usersRigidbody;
    private string name;
    public float rangeForNPC = 5;

    public Weapon(Rigidbody usersRigidbody, string name) {
        this.usersRigidbody = usersRigidbody;
        this.name = name;
    }

    public string getName() {return this.name;}

    public abstract void effect();

    public virtual void idleEffect() {return;}

    public virtual void npcBehaviour(Transform target) {return;}

    protected void aimPlayer(float offset) {
        this.usersRigidbody.angularVelocity = Vector3.zero;
        float angleToMouse = Helpers.angleFromPositionToMouse(this.usersRigidbody.position) + offset;
        this.usersRigidbody.rotation = Quaternion.AngleAxis(angleToMouse, Vector3.up);
    }
}
