using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.02f;

    void Update()
    {
        transform.position += CalculateVelocity();
    }

    void OnCollisionEnter()
    {
        Debug.Log("We hit something");
    }

    void OnTriggerEnter()
    {
        Debug.Log("We toasd something");
    }

    public Vector3 CalculateVelocity()
    {
        float leftMovement = System.Convert.ToSingle(Input.GetKey("a"));
	    float rightMovement = System.Convert.ToSingle(Input.GetKey("d"));
        float horizontalMovement = (rightMovement - leftMovement);

	    float upMovement = System.Convert.ToSingle(Input.GetKey("w"));
	    float downMovement = System.Convert.ToSingle(Input.GetKey("s"));
        float verticalMovement = (upMovement - downMovement);

	    Vector3 velocity = new Vector3(horizontalMovement, verticalMovement, 0) * moveSpeed;

        if (horizontalMovement != 0 && verticalMovement != 0) {
            // So the player doesn't move faster when travelling diagonally.
            velocity /= System.Convert.ToSingle(System.Math.Sqrt(2));
        }

        return velocity;
    }


}
