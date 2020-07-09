using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Behaviour {
    public NPC npc;

    public Behaviour(NPC npc) {
        this.npc = npc;
    }

    public abstract void doBehaviour();

    public abstract bool isComplete();
}


public class WalkToTargetBehaviour : Behaviour {
    public Vector3 targetPosition;

    public WalkToTargetBehaviour(NPC npc, Vector3 targetPosition) : base(npc) {
        this.targetPosition = targetPosition;
    }

    public override void doBehaviour() {
        Vector3 heading = this.targetPosition - this.npc.rigidbody.transform.position;
        this.npc.MoveWithHeading(heading);
        this.npc.AimInDirection(heading, 90f);
    }

    public override bool isComplete() {
        float distanceToTarget = Vector3.Distance(
            this.npc.rigidbody.transform.position, 
            this.targetPosition
        );
        return distanceToTarget < 5f;
    }
}

public class AttackTargetBehaviour : Behaviour {
    public Character target;
    public Item weapon;

    public AttackTargetBehaviour(NPC npc, Character target, Item weapon) : base(npc){
        this.target = target;
        this.weapon = weapon;
    }

    public override void doBehaviour() {
        float distanceToTarget = Vector3.Distance(
            this.npc.rigidbody.transform.position,
            this.target.rigidbody.position
        );

        if (distanceToTarget < this.weapon.rangeForNPC) {
            this.weapon.effect(this.npc, target.rigidbody.position);
        } else {
            Vector3 heading = this.target.rigidbody.position - this.npc.rigidbody.transform.position;
            this.npc.MoveWithHeading(heading);
            this.npc.AimInDirection(heading, 90f);
        }

        this.weapon.idleEffect();
    }

    public override bool isComplete() {
        return this.target.isDead;
    }
}
