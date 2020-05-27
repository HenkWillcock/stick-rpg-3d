using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDisappear : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            GetComponent<Renderer>().enabled = false;
            Debug.Log("Yo");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            GetComponent<Renderer>().enabled = true;
        }
    }
}
