using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDisappear : MonoBehaviour
{
    public Collider player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            GetComponent<Renderer>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            GetComponent<Renderer>().enabled = true;
        }
    }
}
