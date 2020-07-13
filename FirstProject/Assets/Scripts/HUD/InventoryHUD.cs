using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemSlotPrefab;

    public bool isExpanded;

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
                        this.itemSlots.Add(this.CreateInventorySlot(yOffset, item.getName(), Color.white));
                    } else {
                        this.itemSlots.Add(this.CreateInventorySlot(yOffset, item.getName(), Color.gray));
                    }
                    yOffset -= 35;
                }
            } else {
                this.itemSlots.Add(this.CreateInventorySlot(0, this.inventory.currentItem().getName(), Color.white));
            }
        }
    }

    public GameObject CreateInventorySlot(float yOffset, string displayText, Color color) {
        GameObject newItemSlot = Object.Instantiate(
            this.itemSlotPrefab,
            new Vector3(0, yOffset, 0),
            new Quaternion(0, 0, 0, 0),
            this.transform
        );
        newItemSlot.GetComponent<RectTransform>().localPosition = new Vector3(0, yOffset, 0);

        ItemSlotHUD newItemSlotScript = newItemSlot.GetComponent<ItemSlotHUD>();

        newItemSlotScript.itemNameText.text = displayText;
        newItemSlotScript.panel.color = color;

        return newItemSlot;
    }

    public void ExpandFor1Second() {
        if (this.minimizeCoroutine != null) {
            StopCoroutine(this.minimizeCoroutine);
        }
        this.isExpanded = true;
        this.minimizeCoroutine = StartCoroutine(this.MinimizeAfter1Second());
    }

    IEnumerator MinimizeAfter1Second() {
        yield return new WaitForSeconds(1);
        this.isExpanded = false;
        this.UpdateInventorySlots();
    }
}
