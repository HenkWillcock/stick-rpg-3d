using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> items;
    public int currentItemIndex;
    public int money;

    public bool isExpanded;

    public Inventory() {
        this.items = new List<Item>();
        this.currentItemIndex = 0;
        this.money = 0;
    }

    public Item currentItem() {
        return this.items[this.currentItemIndex];
    }

    public string InventoryText() {
        if (this.isExpanded) {
            string inventoryString = "$ " + this.money.ToString() + "\n\n";

            foreach (Item item in items) {
                inventoryString += item.getName() + "\n";
            }

            return inventoryString;
        } else {
            return "$ " + this.money.ToString() + "\n\nInventory\n'Tab' to Open";
        }
    }

    public float InventorySize() {
        float numberOfLines = 2;

        if (this.isExpanded) {
            numberOfLines += this.items.Count;
        } else {
            numberOfLines += 2;
        }

        return 0.2f * numberOfLines;
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
        return this.items.Count > 0;
    }
}
