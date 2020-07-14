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
        this.inventoryHUDBelongsTo.BuyItem(this.item);
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        int itemCost = this.item.GetCost();
        if (itemCost == 0) {
            itemNameText.text = "Loot Item";
        } else {
            itemNameText.text = "Buy For $" + item.moneyValue;
        }
    }
 
    public void OnPointerExit (PointerEventData eventData)
    {
        itemNameText.text = item.getName();
    }
}
