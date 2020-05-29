using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // [SerializeField] private float smoothSpeed;
    [SerializeField] private float lookDistance;
    [SerializeField] private float overheadHeight;
    [SerializeField] private float overheadOffset;

    private GameController gameController;

    void Start()
    {
        this.gameController = GameObject.FindObjectOfType<GameController>();

        transform.position = 
            this.gameController.gameFocus.position +
            new Vector3(0, -this.overheadOffset, -this.overheadHeight);
        transform.LookAt(this.gameController.gameFocus.position);
    }

    void LateUpdate()
    {   
        // Slightly move camera with mouse
        Vector3 mousePosition = Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0);
        Vector3 normalizedMousePosition = mousePosition/Screen.width*lookDistance;

        Vector3 desiredPosition =
            this.gameController.gameFocus.position +
            new Vector3(0, -this.overheadOffset, -this.overheadHeight) +
            normalizedMousePosition;

        transform.position = desiredPosition;
        // TODO figure out smooth following without the vibration
        // transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
