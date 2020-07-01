using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private float senseDistance = 100f;
    private Behaviour behaviour;
    private Weapon weapon;
    private Gender genderAttractedTo;
    private RelationshipList relationships;

    public NPC() {
        this.relationships = new RelationshipList();
        this.behaviour = new DocileBehaviour(this);
    }

    void Start() {
        base.Start();
        InvokeRepeating("ChangeBehaviourIfNecessary", 0f, 1f);
    }

    public override void frameUpdate()
    {
        this.behaviour.doBehaviour();
    }

    void ChangeBehaviourIfNecessary() {
        // Logic for changing behaviour. TODO doesn't need to happen every frame.
        RelationshipList relationshipsInRange = this.relationships.relationshipsInRange(
            this.rigidbody.transform.position,
            this.senseDistance
        );

        foreach (Relationship relationship in relationshipsInRange) {
            if (relationship.friendliness < 25) {
                this.behaviour = new AttackTargetBehaviour(
                    this,
                    relationship.character.transform,
                    this.weapons[0]
                );
            }
        }
    }

    public void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);

        BulletBehaviour bullet = collision.gameObject.GetComponent<BulletBehaviour>();

        if (bullet != null) {
            Relationship relationshipToShooter = this.relationships.getRelationshipForCharacter(bullet.shooter);

            if (relationshipToShooter != null) {
                relationshipToShooter.friendliness -= 5;
            } else {
                Relationship newRelationship = new Relationship(bullet.shooter);
                newRelationship.friendliness -= 5;
                this.relationships.Add(newRelationship);
            }
        }

        Character character = collision.gameObject.GetComponent<Character>();

        if (character != null) {
            Relationship relationshipToAttacker = this.relationships.getRelationshipForCharacter(character);

            if (relationshipToAttacker != null) {
                relationshipToAttacker.friendliness -= 5;
            } else {
                Relationship newRelationship = new Relationship(character);
                newRelationship.friendliness -= 5;
                this.relationships.Add(newRelationship);
            }
        }
    }

    public int sexualAttaction(Character character) {
        if (this.genderAttractedTo == character.gender) {
            return conventionalAttractiveness;
        } else {
            return 0;
        }
    }
}
