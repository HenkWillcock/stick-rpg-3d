using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : Movement
{
    public Transform player;

    private Behaviour behaviour;
    private Weapon weapon;

    void Start() {
        // this.behaviour = new DocileBehaviour();
        this.behaviour = new AttackTargetBehaviour(
            this,
            this.player,
            new Spin(this.rigidbody, "Spin", 15)
        );
    }

    void Update() {
        this.behaviour.doBehaviour();
    }
}

public abstract class Behaviour : MonoBehaviour {
    public Movement movementScript;

    public Behaviour(Movement movementScript) {
        this.movementScript = movementScript;
    }

    public abstract void doBehaviour();
}

public class DocileBehaviour : Behaviour {
    public DocileBehaviour(Movement movementScript) : base(movementScript) {

    }

    public override void doBehaviour() {
        return;
    }
}

public class AttackTargetBehaviour : Behaviour {
    public Transform target;
    public Weapon weapon;

    public AttackTargetBehaviour(Movement movementScript, Transform target, Weapon weapon) : base(movementScript){
        this.target = target;
        this.weapon = weapon;
    }

    public override void doBehaviour() {
        float distanceToTarget = Vector3.Distance(this.movementScript.rigidbody.transform.position, target.position);

        if (distanceToTarget < this.weapon.rangeForNPC) {
            this.weapon.effect();
        } else {
            Vector3 heading = target.position - this.movementScript.rigidbody.transform.position;
            this.movementScript.MoveWithHeading(heading);
        }
    }
}
