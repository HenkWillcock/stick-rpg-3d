using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Rigidbody bulletPrefab;

    private List<Weapon> weapons;
    private int weaponIndex;

    public Weapon currentWeapon() {
        return this.weapons[this.weaponIndex];
    }

    void Start()
    {
        rigidbody.maxAngularVelocity = 30;

        this.weapons = new List<Weapon>();

        this.weapons.Add(new Spin(this.rigidbody, "Spin", 15));
        this.weapons.Add(new Gun(this.rigidbody, "Pistol", this.bulletPrefab, 40, 30));
        this.weapons.Add(new Gun(this.rigidbody, "Machine Gun", this.bulletPrefab, 30, 8));
    }

    void Update()
    {
        // Use weapon.
        if (Input.GetMouseButton(0)) {
            this.currentWeapon().effect();
        } else {
            this.currentWeapon().idleEffect();
            aimPlayer(90);
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
        float angleToMouse = Helpers.angleFromPositionToMouse(this.rigidbody.position) + offset;
        this.rigidbody.rotation = Quaternion.AngleAxis(angleToMouse, Vector3.up);
    }
}

public abstract class Weapon {  // TODO pretty sure this doesn't need to be MonoBehaviour
    protected Rigidbody usersRigidbody;
    private string name;    

    public Weapon(Rigidbody usersRigidbody, string name) {
        this.usersRigidbody = usersRigidbody;
        this.name = name;
    }

    public string getName() {return this.name;}

    public abstract void effect();

    public virtual void idleEffect() {return;}

    public virtual void npcBehaviour(Rigidbody target) {return;}

    protected void aimPlayer(float offset) {
        this.usersRigidbody.angularVelocity = Vector3.zero;
        float angleToMouse = Helpers.angleFromPositionToMouse(this.usersRigidbody.position) + offset;
        this.usersRigidbody.rotation = Quaternion.AngleAxis(angleToMouse, Vector3.up);
    }
}

public class Helpers {
    public static float angleFromPositionToMouse(Vector3 position) {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(position);
        return Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
    }
}
