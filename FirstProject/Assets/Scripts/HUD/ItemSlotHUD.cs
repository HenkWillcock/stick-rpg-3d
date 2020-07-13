using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotHUD : MonoBehaviour
{
    public Item item;
    public InventoryHUD inventoryHUDBelongsTo;

    public Text itemNameText;
    public Image panel;
    public Button button;

    void Start() {
        this.button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() {
        this.inventoryHUDBelongsTo.hudBelongsTo.player.inventory.AddItem(this.item);
        this.inventoryHUDBelongsTo.UpdateInventorySlots();
        this.inventoryHUDBelongsTo.hudBelongsTo.player.inventory.inventoryHud.UpdateInventorySlots();
    }
}
