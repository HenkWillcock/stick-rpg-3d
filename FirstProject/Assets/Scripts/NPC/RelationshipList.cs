using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Relationship {
    public Character character;

    public int friendliness;  // Starts at 5, ranges from 0 - 10.
    public int respect;  // Starts at 0, can go up to 10.

    public Relationship(Character character) {
        this.character = character;
        this.friendliness = 5;
        this.respect = 0;
    }
}


public class RelationshipList : List<Relationship>
{
    public RelationshipList relationshipsInRange(Vector3 position, float senseDistance) {
        RelationshipList relationshipsInRange = new RelationshipList();

        foreach (Relationship relationship in this) {
            float distanceToRelationship = Vector3.Distance(
                position, 
                relationship.character.rigidbody.position
            );
            if (distanceToRelationship < senseDistance) {
                relationshipsInRange.Add(relationship);
            }
        }

        return relationshipsInRange;
    }

    public Relationship getRelationshipForCharacter(Character character) {
        foreach (Relationship relationship in this) {
            if (relationship.character == character) {
                return relationship;
            }
        }
        return null;
    }
}
