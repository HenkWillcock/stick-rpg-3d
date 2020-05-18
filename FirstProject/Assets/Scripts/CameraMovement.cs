using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] public Transform target;
    public float smoothSpeed = 0.125f;
    public float lookDistance = 10;
    public Vector3 zOffset = new Vector3(0, 0, -20);

    // Update is called once per frame
    void LateUpdate()
    {   
        Vector3 mousePosition = Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0);
        Vector3 desiredPosition = target.position + zOffset + mousePosition/Screen.width*lookDistance;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
