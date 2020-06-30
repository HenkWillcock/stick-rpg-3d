using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : Movement
{
    public Transform player = null;  // TODO remove this eventually, just used for testing ATM

    private Behaviour behaviour;
    private Weapon weapon;
    private Gender genderAttractedTo;

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

    public int sexualAttaction(Movement character) {
        if (this.genderAttractedTo == character.gender) {
            return conventionalAttractiveness;
        } else {
            return 0;
        }
    }
}

// public enum Gender {
//     Male,
//     Female,
//     Neither,
//     Both
// }

// public class Character {
//     public Gender gender;

//     public int conventionalAttractiveness;
//     // Ranges from 0 to 10.
//     // Used by NPCs to calculate sexual attaction.
// }

public class Relationship {
    public Movement character;

    public int friendliness;  // Starts at 5, ranges from 0 - 10.
    public int respect;  // Starts at 0, can go up to 10.
}

public abstract class Behaviour {
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
