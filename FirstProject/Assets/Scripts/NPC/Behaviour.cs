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
        this.npc.GoToPosition(this.targetPosition);
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

    public AttackTargetBehaviour(NPC npc, Character target) : base(npc){
        this.target = target;
    }

    public override void doBehaviour() {
        float distanceToTarget = Vector3.Distance(
            this.npc.rigidbody.transform.position,
            this.target.rigidbody.position
        );

        if (distanceToTarget < this.npc.inventory.currentItem().rangeForNPC) {
            this.npc.inventory.currentItem().effect(this.npc, target.rigidbody.position);
        } else {
            this.npc.GoToPosition(this.target.rigidbody.position);
        }

        this.npc.inventory.currentItem().idleEffect();
    }

    public override bool isComplete() {
        return this.target.isDead;
    }
}
