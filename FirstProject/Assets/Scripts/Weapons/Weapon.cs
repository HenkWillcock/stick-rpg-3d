using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon {
    protected Character user;
    private string name;
    public float rangeForNPC;

    public Weapon(Character user, string name) {
        this.user = user;
        this.name = name;
    }

    public string getName() {return this.name;}

    public abstract void effect();

    public virtual void idleEffect() {return;}
}
