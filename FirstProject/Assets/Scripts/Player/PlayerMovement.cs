using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float spinSpeed;
    public Rigidbody rigidbody;

    void Start() {
        rigidbody.maxAngularVelocity = spinSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            rigidbody.angularVelocity = new Vector3(0, 0, spinSpeed);
        } else {
            rigidbody.angularVelocity = Vector3.zero;
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(rigidbody.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rigidbody.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // WASD Movement
        float westMovement = System.Convert.ToSingle(Input.GetKey("a"));
	    float eastMovement = System.Convert.ToSingle(Input.GetKey("d"));
        float eastWestMovement = (eastMovement - westMovement);

	    float northMovement = System.Convert.ToSingle(Input.GetKey("w"));
	    float southMovement = System.Convert.ToSingle(Input.GetKey("s"));
        float northSouthMovement = (northMovement - southMovement);
        
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
