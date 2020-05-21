using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDisappear : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().enabled = false;
    }

    void OnTriggerExit(Collider other)
    {
        GetComponent<Renderer>().enabled = true;
    }
}
