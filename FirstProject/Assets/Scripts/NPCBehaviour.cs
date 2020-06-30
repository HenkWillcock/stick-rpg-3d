using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : Character
{
    public Transform player = null;  // TODO remove this eventually, just used for testing ATM

    private bool stunned;
    private float senseDistance = 100f;
    private Behaviour behaviour;
    private Weapon weapon;
    private Gender genderAttractedTo;
    private List<Relationship> relationships;

    public NPCBehaviour() {
        this.relationships = new List<Relationship>();
    }

    void Start() {
        base.Start();

        // this.behaviour = new DocileBehaviour();
        this.behaviour = new AttackTargetBehaviour(
            this,
            this.player,
            new Spin(this, "Spin", 15)
        );

        // Call ChangeBehaviourIfNecessary every 3 seconds.
        InvokeRepeating("ChangeBehaviourIfNecessary", 0f, 3f);

        // TODO figure out how to update relationships.
    }

    public override void subUpdate() {
        if (this.stunned) {
            // TODO set after being attacked.
            return;
        }
        this.behaviour.doBehaviour();
    }

    void ChangeBehaviourIfNecessary() {
        // Logic for changing behaviour. TODO doesn't need to happen every frame.
        List<Relationship> relationshipsInRange = new List<Relationship>();

        foreach (Relationship relationship in this.relationships) {
            float distanceToRelationship = Vector3.Distance(
                this.rigidbody.transform.position, 
                relationship.character.rigidbody.position
            );
            if (distanceToRelationship < this.senseDistance) {
                relationshipsInRange.Add(relationship);
            }
        }

        foreach (Relationship relationship in relationshipsInRange) {
            if (relationship.friendliness < 25) {
                this.behaviour = new AttackTargetBehaviour(
                    this,
                    relationship.character.transform,
                    new Spin(this, "Spin", 15)
                );
            }
        }

        // If close to someone the NPC hates:
        // this.behaviour = new AttackTargetBehaviour(
        //     this,
        //     this.player,
        //     new Spin(this.rigidbody, "Spin", 15)
        // );
    }

    public int sexualAttaction(Character character) {
        if (this.genderAttractedTo == character.gender) {
            return conventionalAttractiveness;
        } else {
            return 0;
        }
    }
}

public class Relationship {
    public Character character;

    public int friendliness;  // Starts at 5, ranges from 0 - 10.
    public int respect;  // Starts at 0, can go up to 10.
}

public abstract class Behaviour {
    public Character movementScript;

    public Behaviour(Character movementScript) {
        this.movementScript = movementScript;
    }

    public abstract void doBehaviour();
}

public class DocileBehaviour : Behaviour {
    public DocileBehaviour(Character movementScript) : base(movementScript) {

    }

    public override void doBehaviour() {
        return;
    }
}

public class AttackTargetBehaviour : Behaviour {
    public Transform target;
    public Weapon weapon;

    public AttackTargetBehaviour(Character movementScript, Transform target, Weapon weapon) : base(movementScript){
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
