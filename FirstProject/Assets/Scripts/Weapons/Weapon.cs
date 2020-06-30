using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon {
    protected Character user;
    private string name;
    public float rangeForNPC = 5;

    public Weapon(Character user, string name) {
        this.user = user;
        this.name = name;
    }

    public string getName() {return this.name;}

    public abstract void effect();

    public virtual void idleEffect() {return;}

    public virtual void npcBehaviour(Transform target) {return;}

    protected void aimPlayer(float offset) {
        this.user.rigidbody.angularVelocity = Vector3.zero;
        float angleToMouse = Helpers.angleFromPositionToMouse(this.user.rigidbody.position) + offset;
        this.user.rigidbody.rotation = Quaternion.AngleAxis(angleToMouse, Vector3.up);
    }
}
