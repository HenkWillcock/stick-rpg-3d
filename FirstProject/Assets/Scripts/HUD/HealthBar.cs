using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public RectTransform healthBarFill;
    public PlayerHealth playerHealth;

    // TODO only call on health change.
    void Update()
    {
        this.healthBarFill.transform.localScale = new Vector3(playerHealth.remainingHealthProportion(), 1, 1);
    }
}
