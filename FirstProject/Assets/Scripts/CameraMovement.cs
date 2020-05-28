using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] public Rigidbody target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float lookDistance = 10;
    [SerializeField] private float height = 15;
    [SerializeField] private float angle = 20;

    private GameController gameController;

    void Start()
    {
        this.gameController = GameObject.FindObjectOfType<GameController>();
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.left);
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        if (gameController.cameraType == "Following") {
            // TODO
        }
        // Position
        Vector3 mousePosition = Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0);

        Vector3 desiredPosition = 
            this.gameController.gameFocus.position +
            - new Vector3(0, Mathf.Tan(angle * Mathf.Deg2Rad) * height, height) + 
            mousePosition/Screen.width*lookDistance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
