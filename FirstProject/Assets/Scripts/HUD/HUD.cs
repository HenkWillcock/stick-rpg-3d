using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Player player;

    public Text itemText;
    public Text vehicleText;
    public RectTransform healthBar;
    public RectTransform staminaBar;
    public RectTransform hungerBar;

    public Text itemsText;
    public RectTransform inventoryPanel;

    public GameObject npcPanel;

    public RectTransform npcHealthBar;
    public Text npcNameText;
    public Text npcItemText;
    public Text npcFriendlinessText;

    public Text npcItemsText;

    void Update()
    {
        this.healthBar.transform.localScale = new Vector3(player.remainingHealthPortion(), 1, 1);
        this.itemText.text = this.player.inventory.currentItem().getName();

        this.itemsText.text = this.player.inventory.InventoryText();
        this.inventoryPanel.transform.localScale = new Vector3(1, this.player.inventory.InventorySize(), 1);

        if (this.player.vehicle != null) {
            this.vehicleText.text = this.player.vehicle.vehicleText();
        } else {
            this.vehicleText.text = "";
        }


        if (Input.GetKeyUp("tab")) {
            this.player.inventory.isExpanded = !this.player.inventory.isExpanded;
        }

        if (this.player.npcClicked != null) {
            

            this.npcPanel.SetActive(true);
            this.npcNameText.text = this.player.npcClicked.name;
            this.npcHealthBar.transform.localScale = new Vector3(
                this.player.npcClicked.remainingHealthPortion(), 1, 1
            );
            this.npcItemText.text = this.player.npcClicked.inventory.currentItem().getName();

            Relationship relationshipToPlayer = 
                this.player.npcClicked.relationships.getRelationshipForCharacter(this.player);

            this.npcFriendlinessText.text = "Friendly: " + relationshipToPlayer.friendliness;

            this.npcItemsText.text = this.player.npcClicked.inventory.InventoryText();

            // TODO see inventory and either buy or pickpocket things, 
            // or if NPC is dead can loot (but this is illegal)

        } else {
            this.npcPanel.SetActive(false);
        }
    }
}
