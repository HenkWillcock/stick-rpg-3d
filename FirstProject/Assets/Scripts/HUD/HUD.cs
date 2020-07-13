using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Player player;

    public Text vehicleText;
    public GameObject vehiclePanel;

    public RectTransform healthBar;
    public RectTransform staminaBar;
    public RectTransform hungerBar;

    public InventoryHUD playerInventoryHud;

    public GameObject npcPanel;

    public RectTransform npcHealthBar;
    public Text npcNameText;
    public Text npcItemText;
    public Text npcFriendlinessText;

    public InventoryHUD npcInventory;

    void Start()
    {
        this.playerInventoryHud.SetInventory(this.player.inventory);
    }

    void Update()
    {
        this.healthBar.transform.localScale = new Vector3(player.remainingHealthPortion(), 1, 1);

        if (this.player.vehicle != null) {
            this.vehiclePanel.SetActive(true);
            this.vehicleText.text = this.player.vehicle.vehicleText();
        } else {
            this.vehiclePanel.SetActive(false);
        }

        if (this.player.npcClicked != null) {

            this.npcPanel.SetActive(true);
            this.npcNameText.text = this.player.npcClicked.name;
            this.npcHealthBar.transform.localScale = new Vector3(
                this.player.npcClicked.remainingHealthPortion(), 1, 1
            );

            // TODO throws Index out of range exception
            Item currentItem = this.player.npcClicked.inventory.currentItem();

            if (currentItem != null) {
                this.npcItemText.text = currentItem.getName();
            }

            Relationship relationshipToPlayer = 
                this.player.npcClicked.relationships.getRelationshipForCharacter(this.player);

            this.npcFriendlinessText.text = "Friendly: " + relationshipToPlayer.friendliness;
        } else {
            this.npcPanel.SetActive(false);
        }
    }
}
