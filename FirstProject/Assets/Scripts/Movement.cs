using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpForce;

    public Rigidbody rigidbody;

    protected int timeUntilJump = 0;

    public Gender gender;

    public int conventionalAttractiveness;
    // Ranges from 0 to 10.
    // Used by NPCs to calculate sexual attaction.

    public void MoveWithHeading(Vector3 heading) {
        float horizontalSpeed = Mathf.Sqrt(
            Mathf.Pow(this.rigidbody.velocity.x, 2) +
            Mathf.Pow(this.rigidbody.velocity.z, 2)
        );

        if (horizontalSpeed < this.topSpeed) {
            heading.y = 0;
            heading.Normalize();
            rigidbody.AddForce(
                heading*acceleration*(this.topSpeed - horizontalSpeed)/this.topSpeed,
                ForceMode.Impulse
            );
        }
    }

    public void JumpIfGrounded() {
        // TODO use code to find players feet
        Vector3 playersFeet = this.rigidbody.position;
        bool isGrounded = Physics.Raycast(playersFeet, Vector3.down, 1.5f);

        if (isGrounded) {
            rigidbody.AddForce(
                new Vector3(0, this.jumpForce, 0),
                ForceMode.Impulse
            );
        }
    }
}

public enum Gender {
    Male,
    Female,
    Neither,
    Both
}
