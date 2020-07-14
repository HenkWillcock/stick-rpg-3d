using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Rigidbody rigidbody;
    public string name;

    public virtual float getBaseDamage() {
        return 0;
    }
}
