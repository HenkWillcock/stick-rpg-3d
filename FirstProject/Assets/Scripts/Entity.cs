using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Put Rigidbody here

    public virtual float getBaseDamage() {
        return 0;
    }
}
