using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDisappear : MonoBehaviour
{
    Transform transform;

    void Start() {
        // BoxCollider triggerBox = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        // triggerBox.isTrigger = true;

        // triggerBox.transform.position = new Vector3(
        //     triggerBox.transform.position.x,
        //     triggerBox.transform.position.y - 1,
        //     triggerBox.transform.position.z
        // );
        // TODO set triggerBox.transform.
    }

    void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null) {
            setRenderersEnabled(false);
            player.GoIndoors();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null) {
            setRenderersEnabled(true);
            player.GoOutdoors();
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

