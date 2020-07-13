using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> items;
    public int currentItemIndex;
    public int money;
    public InventoryHUD inventoryHud;

    private List<GameObject> itemSlots;

    public Inventory() {
        this.items = new List<Item>();
        this.itemSlots = new List<GameObject>();
        this.currentItemIndex = 0;
        this.money = 0;
    }

    public Item currentItem() {
        return this.items[this.currentItemIndex];
    }

    public void selectNextItem() {
        this.currentItemIndex++;
        if (this.currentItemIndex > this.items.Count - 1) {
            this.currentItemIndex -= this.items.Count;
        }
        if (this.inventoryHud != null) {
            this.inventoryHud.ExpandFor1Second();
            this.inventoryHud.UpdateInventorySlots();
        }
    }

    public void selectPreviousItem() {
        this.currentItemIndex--;
        if (this.currentItemIndex < 0) {
            this.currentItemIndex += this.items.Count;
        }
        if (this.inventoryHud != null) {
            this.inventoryHud.ExpandFor1Second();
            this.inventoryHud.UpdateInventorySlots();
        }
    }

    public bool isEmpty() {
        return this.items.Count == 0;
    }

    public void SwitchToBestWeapon() {
        for (int i = 0; i < this.items.Count; i++) {
            if (this.items[i].moneyValue > this.currentItem().moneyValue) {
                this.currentItemIndex = i;
            }
        }
        if (this.inventoryHud != null) {
            this.inventoryHud.UpdateInventorySlots();
        }
    }

    public void SetInventoryHUD(InventoryHUD inventoryHud) {
        foreach (GameObject itemSlot in this.itemSlots) {
            GameObject.Destroy(itemSlot);
        }
        this.inventoryHud = inventoryHud;
        inventoryHud.inventory = this;
        this.inventoryHud.UpdateInventorySlots();
    }
}
