using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDisappear : MonoBehaviour
{
    Transform transform;

    void Start() {
        BoxCollider triggerBox = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        triggerBox.isTrigger = true;
        // TODO set triggerBox.transform.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            setRenderersEnabled(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            setRenderersEnabled(true);
        }
    }

    void setRenderersEnabled(bool enabled) {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null) {
            GetComponent<Renderer>().enabled = enabled;
        }

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].enabled = enabled;
        }
    }
}

