using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDisappear : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Renderer>().enabled = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<Renderer>().enabled = true;
    }
}
