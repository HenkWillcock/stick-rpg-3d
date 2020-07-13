using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour
{
    public HUD hudBelongsTo;

    public Inventory inventory;
    public GameObject itemSlotPrefab;

    public bool isExpanded = true;

    private List<GameObject> itemSlots;
    private Coroutine minimizeCoroutine;

    public void UpdateInventorySlots() {
        if (this.itemSlots != null) {
            foreach (GameObject itemSlot in this.itemSlots) {
                GameObject.Destroy(itemSlot);
            }
        }

        this.itemSlots = new List<GameObject>();

        if (this.inventory != null) {
            if (this.isExpanded) {
                float yOffset = 0;

                foreach (Item item in this.inventory.items) {
                    if (item == this.inventory.currentItem()) {
                        this.itemSlots.Add(this.CreateInventorySlot(yOffset, item, Color.white));
                    } else {
                        this.itemSlots.Add(this.CreateInventorySlot(yOffset, item, Color.gray));
                    }
                    yOffset -= 35;
                }
            } else {
                this.itemSlots.Add(this.CreateInventorySlot(0, this.inventory.currentItem(), Color.white));
            }
        }
    }

    public GameObject CreateInventorySlot(float yOffset, Item item, Color color) {
        GameObject newItemSlot = Object.Instantiate(
            this.itemSlotPrefab,
            new Vector3(0, yOffset, 0),
            new Quaternion(0, 0, 0, 0),
            this.transform
        );
        newItemSlot.GetComponent<RectTransform>().localPosition = new Vector3(0, yOffset, 0);

        ItemSlotHUD newItemSlotScript = newItemSlot.GetComponent<ItemSlotHUD>();

        newItemSlotScript.itemNameText.text = item.getName();
        newItemSlotScript.panel.color = color;
        newItemSlotScript.item = item;
        newItemSlotScript.inventoryHUDBelongsTo = this;

        return newItemSlot;
    }

    public void ExpandFor1Second() {
        if (this.minimizeCoroutine != null) {
            StopCoroutine(this.minimizeCoroutine);
        }
        this.isExpanded = true;
        this.minimizeCoroutine = StartCoroutine(this.MinimizeAfter1Second());
        this.UpdateInventorySlots();
    }

    IEnumerator MinimizeAfter1Second() {
        yield return new WaitForSeconds(1);
        this.isExpanded = false;
        this.UpdateInventorySlots();
    }

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;
        inventory.inventoryHud = this;
        this.UpdateInventorySlots();
    }
}
