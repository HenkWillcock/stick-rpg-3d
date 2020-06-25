using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDText : MonoBehaviour
{
    public Text hudText;
    public ReturnsText scriptWithText;

    void Update()
    {
        this.hudText.text = this.scriptWithText.outputText();
    }
}
