using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    private enum State {
        Docile,
        Stunned,
        Fleeing,
        Attacking
    }

    [SerializeField] private State state;

    void Update() {
        if (state == State.Attacking) {
            
        }
    }
}
