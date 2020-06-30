using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // [SerializeField] private float smoothSpeed;
    [SerializeField] private float lookDistance;

    private Vector3 overheadHeight;
    private float overheadOffset;

    private GameController gameController;

    public bool overheadMode;

    void Start()
    {
        this.gameController = GameObject.FindObjectOfType<GameController>();
    }

    void LateUpdate()
    {
        if(this.overheadMode) {
            transform.position = this.defaultOverheadPosition() + overheadLookAround();
        } else {
            transform.position = 
                    this.gameController.gameFocus.rigidbody.position +
                    this.overheadHeight -
                    this.gameController.gameFocus.transform.forward * this.overheadOffset;
            transform.LookAt(this.gameController.gameFocus.rigidbody.position);
        }

        // TODO figure out smooth following without the vibration
        // transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }

    public void recalculateCameraPosition(float angle, float distance) {
        this.overheadOffset = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;
        this.overheadHeight = new Vector3(
            0, Mathf.Cos(angle * Mathf.Deg2Rad) * distance, 0
        );

        transform.position = defaultOverheadPosition();

        transform.LookAt(this.gameController.gameFocus.rigidbody.position);
    }

    private Vector3 defaultOverheadPosition() {
        return
            this.gameController.gameFocus.rigidbody.position +
            this.overheadHeight + 
            this.defaultOffset();
    }

    private Vector3 defaultOffset() {
        return new Vector3(0, 0, -this.overheadOffset);
    }

    private Vector3 overheadLookAround() {
        Vector3 mousePosition = new Vector3(
            Input.mousePosition.x - Screen.width/2, 
            0,
            Input.mousePosition.y -Screen.height/2
        );

        return mousePosition/Screen.width*lookDistance;
    }
}
