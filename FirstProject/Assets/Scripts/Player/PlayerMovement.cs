using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    private int timeUntilJump = 0;
    private Rigidbody vehicle;

    void Update()
    {
        // WASD Movement
        float leftRightMovement = 
                System.Convert.ToSingle(Input.GetKey("d")) -
                System.Convert.ToSingle(Input.GetKey("a"));

        float upDownMovement = 
                System.Convert.ToSingle(Input.GetKey("w")) -
                System.Convert.ToSingle(Input.GetKey("s"));

        this.MoveWithHeading(new Vector3(leftRightMovement, 0, upDownMovement));

        // Jumping
        if (Input.GetKey("space") && this.timeUntilJump == 0) {
            this.JumpIfGrounded();
            this.timeUntilJump = 5;
        } else if (this.timeUntilJump > 0) {
            this.timeUntilJump--;
        }
    }
}
