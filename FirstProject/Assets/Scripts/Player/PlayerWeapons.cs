using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : ReturnsText
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
        this.weapons.Add(new Gun(this.rigidbody, "Pistol", this.bulletPrefab, 30, 30));
        this.weapons.Add(new Gun(this.rigidbody, "Machine Gun", this.bulletPrefab, 30, 8));
        this.weapons.Add(new Gun(this.rigidbody, "Sniper", this.bulletPrefab, 80, 60));
    }

    void Update()
    {
        // Use Weapon
        if (Input.GetMouseButton(0)) {
            this.currentWeapon().effect();
        } else {
            this.currentWeapon().idleEffect();
            aimPlayer(90);
        }

        // Select Next Weapon
        if (Input.mouseScrollDelta.y > 0) {
            this.weaponIndex++;
        } else if (Input.mouseScrollDelta.y < 0) {
            this.weaponIndex--;
        }

        // Loop Weapon Index Around
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

    public override string outputText() {
        return this.currentWeapon().getName();
    }
}

public class Helpers {
    public static float angleFromPositionToMouse(Vector3 position) {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(position);
        return Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
    }
}
