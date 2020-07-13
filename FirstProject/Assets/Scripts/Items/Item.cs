using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public Inventory inventoryBelongsTo;

    private string name;
    public int moneyValue;
    public float chanceNPCHas;
    public float rangeForNPC;

    public Item(string name, int moneyValue, float chanceNPCHas) {
        this.name = name;
        this.moneyValue = moneyValue;
        this.chanceNPCHas = chanceNPCHas;
    }

    public string getName() {return this.name;}

    public virtual void effect(Character user, Vector3 targetPosition) {}

    public virtual void idleEffect() {}
}
