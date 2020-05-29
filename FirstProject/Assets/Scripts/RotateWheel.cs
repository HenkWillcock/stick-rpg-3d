using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public Transform transform;
    public WheelCollider wheelCollider;

    // Update is called once per frame
    void Update()
    {
        float wheelRpm = wheelCollider.rpm * Time.deltaTime * 3;
        transform.RotateAround(transform.position, transform.up, -wheelRpm);
    }
}
