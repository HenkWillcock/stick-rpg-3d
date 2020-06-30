using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalStatBar : MonoBehaviour
{
    public RectTransform barFill;
    public PlayerVitalStat playerVitalStat;

    // TODO only call on stat change.
    // TODO replace this with HUD.cs script
    void Update()
    {
        this.barFill.transform.localScale = new Vector3(playerVitalStat.remainingProportion(), 1, 1);
    }
}
