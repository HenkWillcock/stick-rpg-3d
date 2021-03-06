using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private float senseDistance;
    private Behaviour behaviour;
    private Gender genderAttractedTo;

    public RelationshipList relationships;

    void Start() {
        base.Start();
        this.relationships = new RelationshipList();
        this.senseDistance = 30f;
        InvokeRepeating("ChangeBehaviourIfNecessary", 0f, 1f);
    }

    public override void frameUpdate()
    {
        if (this.hasBehaviour()) {
            this.behaviour.doBehaviour();
            if (this.behaviour.isComplete()) {
                this.behaviour = null;
            }
        } else {
            this.AimInDirection(new Vector3(0, 0, 0), 0f);
        }
    }

    void ChangeBehaviourIfNecessary() {
        if (this.isDead) {
            return;
        }

        RelationshipList relationshipsInRange = this.relationships.relationshipsInRange(
            this.rigidbody.transform.position,
            this.senseDistance
        );

        // Attack Nearby Enemy Characters
        foreach (Relationship relationship in relationshipsInRange) {
            if (relationship.friendliness <= 15) {
                Item weapon;

                if (!this.inventory.isEmpty()) {
                    this.inventory.SwitchToBestWeapon();
                }

                this.behaviour = new AttackTargetBehaviour(
                    this,
                    relationship.character
                );
            }
        }

        // TODO run from Enemies if this NPC is a wuss

        // Randomly Walk Somewhere
        if (!this.hasBehaviour()) {
            if (Random.Range(0f, 1f) > 0.9f) {
                this.behaviour = new WalkToTargetBehaviour(
                    this,
                    new Vector3(
                        this.rigidbody.position.x + Random.Range(-20f, 20f),
                        5,
                        this.rigidbody.position.z + Random.Range(-20f, 20f)
                    )
                );
            }
        }
    }

    public override void DoDamageActions(Character attacker, float damageAmount) {
        base.DoDamageActions(attacker, damageAmount);

        int damageInt = System.Convert.ToInt32(damageAmount);

        // TODO throws null pointer exception
        this.relationships.getRelationshipForCharacter(attacker).changeFriendliness(-damageInt);  
    }

    public int sexualAttaction(Character character) {
        if (this.genderAttractedTo == character.gender) {
            return conventionalAttractiveness;
        } else {
            return 0;
        }
    }

    private bool hasBehaviour() {
        return this.behaviour != null;
    }

    public void GoToPosition(Vector3 targetPosition) {
        // This method should incorporate vehicles and public transport for longer journeys.
        Vector3 heading = targetPosition - this.rigidbody.transform.position;
        this.MoveWithHeading(heading);
        this.AimInDirection(heading, 90f);
    }

    public override void setDead() {
        base.setDead();
        this.behaviour = null;
    }
}
