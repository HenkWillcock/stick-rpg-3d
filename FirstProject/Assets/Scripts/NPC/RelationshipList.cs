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
        return null;
    }

    public void changeCharacterFriendliness(Character character, int friendlinessChange) {
        Relationship relationshipToCharacter = this.getRelationshipForCharacter(character);

        if (relationshipToCharacter != null) {
            relationshipToCharacter.friendliness += friendlinessChange;
        } else {
            Relationship newRelationship = new Relationship(character);
            newRelationship.friendliness += friendlinessChange;
            this.Add(newRelationship);
        }
    }
}
