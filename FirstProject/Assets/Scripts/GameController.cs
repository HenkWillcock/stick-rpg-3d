using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Rigidbody gameFocus;
    public Rigidbody player;

    public GameObject overheadCameraPrefab;
    public GameObject followingCameraPrefab;

    public GameObject camera;
    public CameraType cameraType;

    void Start() {
        this.SwitchToOverheadCamera();
    }

    void SwitchToOverheadCamera() {
        this.camera = Instantiate(this.overheadCameraPrefab);
    }

    void SwitchToFollowingCamera() {
        this.camera = Instantiate(this.followingCameraPrefab);
    }
}
