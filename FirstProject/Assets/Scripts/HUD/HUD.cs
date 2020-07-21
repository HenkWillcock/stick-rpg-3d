using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Player player;

    public Text vehicleText;

    public RectTransform healthBar;
    public RectTransform staminaBar;
    public RectTransform hungerBar;
    public Text moneyText;

    public InventoryHUD playerInventoryHud;

    public GameObject npcPanel;

    public RectTransform npcHealthBar;
    public Text npcNameText;
    public Text npcItemText;
    public Text npcMoneyText;
    public Text npcFriendlinessText;
    

    public InventoryHUD npcInventory;

    void Start()
    {
        this.playerInventoryHud.SetInventory(this.player.inventory);
    }

    void Update()
    {
        this.healthBar.transform.localScale = new Vector3(player.remainingHealthPortion(), 1, 1);

        this.moneyText.text = "$ " + this.player.inventory.money.ToString();

        if (this.player.entityClicked != null) {

            this.npcPanel.SetActive(true);
            this.npcNameText.text = this.player.entityClicked.name;

            if (this.player.entityClicked is HealthEntity healthEntity) {
                this.npcHealthBar.gameObject.SetActive(true);

                this.npcHealthBar.transform.localScale = new Vector3(
                    healthEntity.remainingHealthPortion(), 1, 1
                );
            } else {
                this.npcHealthBar.gameObject.SetActive(false);
            }

            if (this.player.entityClicked is NPC npc) {
                // TODO throws Index out of range exception
                Item currentItem = npc.inventory.currentItem();

                if (currentItem != null) {
                    this.npcItemText.text = currentItem.getName();
                }
                this.npcMoneyText.text = "$ " + npc.inventory.money.ToString();

                Relationship relationshipToPlayer = 
                    npc.relationships.getRelationshipForCharacter(this.player);

                this.npcFriendlinessText.text = "Friendly: " + relationshipToPlayer.friendliness;

            } else {
                this.npcItemText.text = "";
                this.npcFriendlinessText.text = "";
                this.npcMoneyText.text = "";
            }

            if (this.player.entityClicked is Vehicle vehicle) {
                this.vehicleText.text = vehicle.vehicleText();
            } else {
                this.vehicleText.text = "";
            }

        } else {
            this.npcPanel.SetActive(false);
        }
    }
}
