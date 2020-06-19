using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    public Rigidbody rigidbody;
    public Rigidbody vehicle;

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

        float jump = System.Convert.ToSingle(Input.GetKey("space"));

	    rigidbody.velocity = new Vector3(
            eastWestMovement*moveSpeed,
            northSouthMovement*moveSpeed,
            rigidbody.velocity.z - jump*jumpForce
        );
    }
}
