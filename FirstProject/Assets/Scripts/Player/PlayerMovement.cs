using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpForce;

    public Rigidbody playerRigidbody;

    private int timeUntilJump = 0;
    private Rigidbody vehicle;

    void Update()
    {
        // WASD Movement
        float westMovement = System.Convert.ToSingle(Input.GetKey("a"));
	    float eastMovement = System.Convert.ToSingle(Input.GetKey("d"));
        float eastWestMovement = eastMovement - westMovement;

	    float northMovement = System.Convert.ToSingle(Input.GetKey("w"));
	    float southMovement = System.Convert.ToSingle(Input.GetKey("s"));
        float northSouthMovement = northMovement - southMovement;

        // So the player doesn't move faster when travelling diagonally.
        if (eastWestMovement != 0 && northSouthMovement != 0) {    
            eastWestMovement /= System.Convert.ToSingle(System.Math.Sqrt(2));
            northSouthMovement /= System.Convert.ToSingle(System.Math.Sqrt(2));
        }

        Vector3 playersFeet = this.playerRigidbody.position + new Vector3(0, 0, 1.1f);
        bool isGrounded = Physics.Raycast(playersFeet, Vector3.forward, 0.5f);
        float jump = 0;

        if (this.timeUntilJump == 0) {
            jump = System.Convert.ToSingle(Input.GetKey("space"));
            this.timeUntilJump = 5;
        } else {
            this.timeUntilJump--;
        }
        

        float horizontalSpeed = Mathf.Sqrt(
            Mathf.Pow(this.playerRigidbody.velocity.x, 2) +
            Mathf.Pow(this.playerRigidbody.velocity.y, 2)
        );

	    playerRigidbody.AddForce(
            new Vector3(
                eastWestMovement*acceleration*(this.topSpeed - horizontalSpeed)/this.topSpeed,
                northSouthMovement*acceleration*(this.topSpeed - horizontalSpeed)/this.topSpeed,
                -jump*this.jumpForce*System.Convert.ToSingle(isGrounded)
            ),
            ForceMode.Impulse
        );
    }
}
