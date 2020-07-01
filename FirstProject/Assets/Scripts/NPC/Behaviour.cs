using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Behaviour {
    public NPC npc;

    public Behaviour(NPC npc) {
        this.npc = npc;
    }

    public abstract void doBehaviour();
}


public class DocileBehaviour : Behaviour {
    public DocileBehaviour(NPC npc) : base(npc) {}

    public override void doBehaviour() {
        this.npc.AimInDirection(new Vector3(0, 0, 0), 0f);
    }
}

public class AttackTargetBehaviour : Behaviour {
    public Transform target;
    public Weapon weapon;

    public AttackTargetBehaviour(NPC npc, Transform target, Weapon weapon) : base(npc){
        this.target = target;
        this.weapon = weapon;
    }

    public override void doBehaviour() {
        float distanceToTarget = Vector3.Distance(this.npc.rigidbody.transform.position, target.position);

        if (distanceToTarget < this.weapon.rangeForNPC) {
            this.weapon.effect();
        } else {
            Vector3 heading = target.position - this.npc.rigidbody.transform.position;
            this.npc.MoveWithHeading(heading);
            this.npc.AimInDirection(heading, 90f);
        }
    }
}
