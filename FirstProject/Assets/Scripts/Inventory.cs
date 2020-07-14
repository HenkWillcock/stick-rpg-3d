using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : IEnumerable
{
    public Character owner;  // Back Ref

    private List<Item> items;
    public int currentItemIndex;
    public int money;
    public InventoryHUD inventoryHud;

    public Inventory() {
        this.items = new List<Item>();
        this.currentItemIndex = 0;
        this.money = 0;
    }

    public Item currentItem() {
        try {
            return this.items[this.currentItemIndex];
        } catch (System.ArgumentOutOfRangeException e) {
            return null;
        }
    }

    public void selectNextItem() {
        this.currentItemIndex++;
        if (this.currentItemIndex > this.items.Count - 1) {
            this.currentItemIndex -= this.items.Count;
        }
    }

    public void selectPreviousItem() {
        this.currentItemIndex--;
        if (this.currentItemIndex < 0) {
            this.currentItemIndex += this.items.Count;
        }
    }

    public bool isEmpty() {
        return this.items.Count == 0;
    }

    public void SwitchToBestWeapon() {
        for (int i = 0; i < this.items.Count; i++) {
            if (this.currentItem() == null) {
                this.currentItemIndex = i;

            } else if (this.items[i].moneyValue > this.currentItem().moneyValue) {
                this.currentItemIndex = i;
            }
        }
        if (this.inventoryHud != null) {
            this.inventoryHud.UpdateInventorySlots();
        }
    }

    public void AddItem(Item item) {
        if (item.inventoryBelongsTo != null) {
            item.inventoryBelongsTo.items.Remove(item);
        }
        this.items.Add(item);
        item.inventoryBelongsTo = this;
        if (this.inventoryHud != null) {
            this.inventoryHud.UpdateInventorySlots();
        }
    }

    public void BuyItem(Item item) {
        if (item.GetCost() <= this.money) {
            this.money -= item.GetCost();
            item.inventoryBelongsTo.money += item.GetCost();
            this.AddItem(item);
        }
    }

    public void UnequipItem() {
        this.currentItemIndex = -1;
        if (this.inventoryHud != null) {
            this.inventoryHud.UpdateInventorySlots();
        }
    }

    public IEnumerator GetEnumerator()
    {
        return this.items.GetEnumerator();
    }
}
