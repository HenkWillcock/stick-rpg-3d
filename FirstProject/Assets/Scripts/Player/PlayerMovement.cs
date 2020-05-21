using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.02f;
    [SerializeField] private float jumpForce = 2.0f;
    public Rigidbody rigidbody;

    void Update()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(rigidbody.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigidbody.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // WASD Movement
        float westMovement = System.Convert.ToSingle(Input.GetKey("a"));
	    float eastMovement = System.Convert.ToSingle(Input.GetKey("d"));
        float eastWestMovement = (eastMovement - westMovement);

	    float northMovement = System.Convert.ToSingle(Input.GetKey("w"));
	    float southMovement = System.Convert.ToSingle(Input.GetKey("s"));
        float northSouthMovement = (northMovement - southMovement);

        if (eastWestMovement != 0 && northSouthMovement != 0) {
            // So the player doesn't move faster when travelling diagonally.
            eastWestMovement /= System.Convert.ToSingle(System.Math.Sqrt(2));
            northSouthMovement /= System.Convert.ToSingle(System.Math.Sqrt(2));
        }

        float jump = System.Convert.ToSingle(Input.GetKey("space"));

	    rigidbody.velocity = new Vector3(
            eastWestMovement*moveSpeed,
            northSouthMovement*moveSpeed,
            rigidbody.velocity.z - jump*jumpForce
        );
    }
}
