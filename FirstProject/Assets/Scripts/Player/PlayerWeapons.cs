using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private int weaponIndex;
    [SerializeField] private float spinSpeed;

    public Rigidbody rigidbody;
    public Rigidbody bulletPrefab;

    void Start()
    {
        rigidbody.maxAngularVelocity = spinSpeed;
        weapons = new List<Weapon>();
        weapons.Add(new Spin(this.rigidbody, this.spinSpeed));
        weapons.Add(new Pistol(this.rigidbody, this.bulletPrefab));
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

        while (this.weaponIndex > this.weapons.Count - 1) {
            this.weaponIndex -= this.weapons.Count;
        }
    
        while (weaponIndex < 0) {
            this.weaponIndex += this.weapons.Count;
        }
    }

    protected void aimPlayer(float offset) {
        this.rigidbody.angularVelocity = Vector3.zero;
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(rigidbody.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + offset;
        this.rigidbody.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(this.playerRigidbody.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + offset;
        this.playerRigidbody.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

public class Spin : Weapon {
    float spinSpeed;

    public Spin(Rigidbody playerRigidbody, float spinSpeed) : base(playerRigidbody) {
        this.spinSpeed = spinSpeed;
    }

    public override void effect() {
        this.playerRigidbody.angularVelocity = new Vector3(0, 0, this.spinSpeed);
    }
}

public class Pistol : Weapon {
    public Rigidbody bulletPrefab;

    public Pistol(Rigidbody playerRigidbody, Rigidbody bulletPrefab) : base(playerRigidbody) {
        this.bulletPrefab = bulletPrefab;
    }

    public override void effect() {
        aimPlayer(-90);

        Rigidbody bullet;
        bullet = Instantiate(this.bulletPrefab, this.playerRigidbody.position, this.playerRigidbody.rotation);
    }
}
