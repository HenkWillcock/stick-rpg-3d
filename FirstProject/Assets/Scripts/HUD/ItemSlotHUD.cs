using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
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
        // TODO pass actions into buttons when they're created.
        this.inventoryHUDBelongsTo.BuyItem(this.item);
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        // TODO pass actions into buttons when they're created.
        int salePrice = this.item.GetSalePrice(
            this.inventoryHUDBelongsTo.hudBelongsTo.player
        );
        if (salePrice == 0) {
            itemNameText.text = "Loot Item";
        } else {
            itemNameText.text = "Buy For $" + salePrice;
        }
    }
 
    public void OnPointerExit (PointerEventData eventData)
    {
        itemNameText.text = item.getName();
    }
}
