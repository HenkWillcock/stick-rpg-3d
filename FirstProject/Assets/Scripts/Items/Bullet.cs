using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    public Rigidbody rigidbody;
    public float lifeSpan;

    public Character shooter;  // TODO use this to change relationships when bullet hits.

    void Start() {
        this.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    // Update is called once per frame
    void Update()
    {
        this.lifeSpan -= Time.deltaTime;
        if (this.lifeSpan < 0) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) {
        this.rigidbody.useGravity = true;
    }

    public override float getBaseDamage() {
        return 10;
    }
}
