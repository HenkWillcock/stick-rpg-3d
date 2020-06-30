using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // TODO remove this entirely
    public CameraMovement cameraScript;

    void Start() {
        StartCoroutine(this.LateStart(0.1f));
    }

    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        this.cameraScript.recalculateCameraPosition(30, 25);
    }
}
