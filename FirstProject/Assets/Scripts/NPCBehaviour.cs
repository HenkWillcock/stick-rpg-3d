using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public Rigidbody ownRigidbody;
    public Rigidbody player;

    private Behaviour behaviour;
    private Weapon weapon;

    void Start() {
        this.behaviour = new DocileBehaviour();
        // this.behaviour = new AttackTargetBehaviour(
        //     this.player,
        //     new Spin(this.ownRigidbody, "Spin", 15)
        // );
    }

    void Update() {
        this.behaviour.doBehaviour();
    }
}

public abstract class Behaviour : MonoBehaviour {
    public abstract void doBehaviour();
}

public class DocileBehaviour : Behaviour {
    public override void doBehaviour() {
        return;
    }
}

public class AttackTargetBehaviour : Behaviour {
    public Rigidbody target;
    public Weapon weapon;

    public AttackTargetBehaviour(Rigidbody target, Weapon weapon) {
        this.target = target;
        this.weapon = weapon;
    }

    public override void doBehaviour() {
        this.weapon.npcBehaviour(this.target);
    }
}
