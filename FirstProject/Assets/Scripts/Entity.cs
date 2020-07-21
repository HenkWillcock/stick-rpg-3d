using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string name;

    private bool isHighlighted;
    private Color color;

    public virtual float getBaseDamage() {
        return 0;
    }

    public void ChangeToColor(Color color) {
        this.color = color;
        this.ChangeRendererColors();
    }

    public void SetIsHighlighted(bool isHighlighted) {
        this.isHighlighted = isHighlighted;
        this.ChangeRendererColors();
    }

    private void ChangeRendererColors() {
        Color displayedColor;

        if (this.isHighlighted) {
            displayedColor = new Color(
                this.color.r + 0.4f,
                this.color.g + 0.4f,
                this.color.b + 0.4f
            );        
        } else {
            displayedColor = this.color;
        }

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) {
            if (renderer.gameObject.GetComponent<ChangesColor>() != null) {
                renderer.material.color = displayedColor;
            }
        }
    }
}
