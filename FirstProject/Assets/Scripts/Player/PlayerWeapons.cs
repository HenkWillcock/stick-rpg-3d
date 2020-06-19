using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private int weaponIndex;

    public Rigidbody rigidbody;

    void Start()
    {
        rigidbody.maxAngularVelocity = 30;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            this.weapons[this.weaponIndex].effect();
        } else {
            aimPlayer(0);
        }

        if (Input.mouseScrollDelta.y > 0) {
            this.weaponIndex++;
        } else if (Input.mouseScrollDelta.y < 0) {
            this.weaponIndex--;
        }

        if (this.weapons.Count > 0) {
            while (this.weaponIndex > this.weapons.Count - 1) {
                this.weaponIndex -= this.weapons.Count;
            }
        
            while (weaponIndex < 0) {
                this.weaponIndex += this.weapons.Count;
            }
        }
    }

    protected void aimPlayer(float offset) {
        this.rigidbody.angularVelocity = Vector3.zero;
        float angleToMouse = Helpers.angleToMouse(this.rigidbody.position) + offset;
        this.rigidbody.rotation = Quaternion.AngleAxis(angleToMouse, Vector3.forward);
    }
}


public abstract class Weapon : MonoBehaviour {
    public Rigidbody playerRigidbody;

    public Weapon(Rigidbody playerRigidbody) {
        this.playerRigidbody = playerRigidbody;
    }

    public abstract void effect();

    protected void aimPlayer(float offset) {
        this.playerRigidbody.angularVelocity = Vector3.zero;
        float angleToMouse = Helpers.angleToMouse(this.playerRigidbody.position) + offset;
        this.playerRigidbody.rotation = Quaternion.AngleAxis(angleToMouse, Vector3.forward);
    }
}

public class Helpers {
    public static float angleToMouse(Vector3 position) {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(position);
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
