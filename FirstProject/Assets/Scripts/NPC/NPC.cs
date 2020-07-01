using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private float senseDistance;
    private Behaviour behaviour;
    private Weapon weapon;
    private Gender genderAttractedTo;
    private RelationshipList relationships;

    public NPC() {
        this.relationships = new RelationshipList();
        this.behaviour = null;
        this.senseDistance = 30f;
    }

    void Start() {
        base.Start();
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
        RelationshipList relationshipsInRange = this.relationships.relationshipsInRange(
            this.rigidbody.transform.position,
            this.senseDistance
        );

        // Attack Nearby Enemy Characters
        foreach (Relationship relationship in relationshipsInRange) {
            if (relationship.friendliness <= 15) {
                Weapon weapon;

                if (this.weapons.Count >= 1) {
                    weapon = this.weapons[0];
                } else {
                    weapon = new Spin(this, "Spin", 15);
                }

                this.behaviour = new AttackTargetBehaviour(
                    this,
                    relationship.character,
                    weapon
                );
            }
        }

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

    public override void doOtherDamageEffects(Collision collision, float damageAmount) {
        base.doOtherDamageEffects(collision, damageAmount);

        // Reduce friendliness by damage dealt once sorted.

        BulletBehaviour bullet = collision.gameObject.GetComponent<BulletBehaviour>();
        if (bullet != null) {
            this.relationships.changeCharacterFriendliness(bullet.shooter, -40);
        }

        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null) {
            this.relationships.changeCharacterFriendliness(character, -5);
        }

        Vehicle vehicle = collision.gameObject.GetComponent<Vehicle>();
        if (vehicle != null) {
            this.relationships.changeCharacterFriendliness(vehicle.driver, -40);
        }
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
}
