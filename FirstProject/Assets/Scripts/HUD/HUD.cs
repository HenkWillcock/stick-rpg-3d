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

    public GameObject npcPanel;

    public RectTransform npcHealthBar;
    public Text npcNameText;
    public Text npcItemText;
    public Text npcFriendlinessText;

    void Update()
    {
        this.healthBar.transform.localScale = new Vector3(player.remainingHealthPortion(), 1, 1);
        this.itemText.text = this.player.currentItem().getName();

        if (this.player.vehicle != null) {
            this.vehicleText.text = this.player.vehicle.vehicleText();
        } else {
            this.vehicleText.text = "";
        }

        if (this.player.npcClicked != null) {
            this.npcPanel.SetActive(true);
            this.npcNameText.text = this.player.npcClicked.name;
            this.npcHealthBar.transform.localScale = new Vector3(
                this.player.npcClicked.remainingHealthPortion(), 1, 1
            );
            this.npcItemText.text = this.player.npcClicked.inventory[0].getName();

            Relationship relationshipToPlayer = 
                this.player.npcClicked.relationships.getRelationshipForCharacter(this.player);

            this.npcFriendlinessText.text = "Friendly: " + relationshipToPlayer.friendliness;

            // TODO see inventory and either buy or pickpocket things

        } else {
            this.npcPanel.SetActive(false);
        }
    }
}
