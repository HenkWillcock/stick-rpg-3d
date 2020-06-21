using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private int weaponIndex;

    public Rigidbody rigidbody;

    public Weapon currentWeapon() {
        return this.weapons[this.weaponIndex];
    }

    void Start()
    {
        rigidbody.maxAngularVelocity = 30;
    }

    void Update()
    {
        // Use weapon.
        if (Input.GetMouseButton(0)) {
            this.currentWeapon().effect();
        } else {
            this.currentWeapon().idleEffect();
            aimPlayer(0);
        }

        // Select next weapon.
        if (Input.mouseScrollDelta.y > 0) {
            this.weaponIndex++;
        } else if (Input.mouseScrollDelta.y < 0) {
            this.weaponIndex--;
        }

        // Loop weapon index around.
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
    public string name;
    public Rigidbody playerRigidbody;

    public Weapon(Rigidbody playerRigidbody) {
        this.playerRigidbody = playerRigidbody;
    }

    public abstract void effect();

    public virtual void idleEffect() {return;}

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
