using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Relationship {
    public Character character;

    public int friendliness;  // Starts at 50, ranges from 0 - 100.
    public int respect;  // Starts at 0, can go up to 100.

    public Relationship(Character character) {
        this.character = character;
        this.friendliness = 50;
        this.respect = 0;
    }

    public void changeFriendliness(int change) {
        this.friendliness += change;

        if (this.friendliness < 0) {
            this.friendliness = 0;
        } else if (this.friendliness > 100) {
            this.friendliness = 100;
        }
    }

    public float SalePriceMultiplier() {
        if (friendliness > 20) {
            return 50 / (friendliness - 20);
        } else {
            return float.MaxValue;
        }
    }
}


public class RelationshipList : List<Relationship>
{
    public RelationshipList relationshipsInRange(Vector3 position, float senseDistance) {
        RelationshipList relationshipsInRange = new RelationshipList();

        foreach (Relationship relationship in this) {
            float distanceToRelationship = Vector3.Distance(
                // TODO this sometimes throws NullReferenceException
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
        // If no relationship exists, create one
        Relationship newRelationship = new Relationship(character);
        this.Add(newRelationship);
        return newRelationship;
    }
}
