using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    private string name;
    public int moneyValue;
    public float rangeForNPC;

    public Item(string name) {
        this.name = name;
    }

    public string getName() {return this.name;}

    public virtual void effect(Character user, Vector3 targetPosition) {}

    public virtual void idleEffect() {}
}
