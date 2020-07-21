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

    public int GetSalePrice(Character buyer) {
        // TODO change this based on relationship with the NPC and your intelligence.

        if (inventoryBelongsTo.owner.isDead) {
            return 0;
        } else if (inventoryBelongsTo.owner is NPC npcOwner) {
            Relationship relationshipToBuyer = 
                npcOwner.relationships.getRelationshipForCharacter(buyer);

            return  System.Convert.ToInt32(
                (float) this.moneyValue * relationshipToBuyer.SalePriceMultiplier()
            );
        } else {
            return 0;  // Should never happen
        }
    }

    public static List<Item> GetAllItems() {
        // TODO improve
        List<Item> allItems = new List<Item>();
        allItems.Add(Spin.BASIC_SPIN());
        allItems.Add(Spin.SUPER_SPIN());
        allItems.Add(Gun.PISTOL());
        allItems.Add(Gun.MACHINE_PISTOL());
        allItems.Add(Gun.ASSAULT_RIFLE());
        allItems.Add(Gun.SNIPER());
        allItems.Add(Gun.HEAVY_SNIPER());
        allItems.Add(Shotgun.SHOTGUN());
        allItems.Add(Shotgun.DOUBLE_SHOTGUN());
        allItems.Add(Shotgun.AUTO_SHOTGUN());
        return allItems;
    }
}
