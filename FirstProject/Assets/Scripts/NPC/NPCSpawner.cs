using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject NPCprefab;

    public Rigidbody bulletPrefab;

    void Start()
    {
        for (int i = 0; i <= 30; i++) {
            this.SpawnNPC();
        }
        InvokeRepeating("SpawnNPC", 0f, 10f);
    }

    void SpawnNPC() {
        float worldHeight = 4;
        float x = Random.Range(-50f, 50f);
        float z = Random.Range(-50f, 50f);

        GameObject newNPC = Object.Instantiate(
            this.NPCprefab,
            new Vector3(x, worldHeight, z),
            new Quaternion(0, 0, 0, 0)
        );

        NPC npc = newNPC.GetComponent<NPC>();

        if (Random.Range(0f, 1f) > 0.5f) {
            npc.weapons.Add(new Gun(
                npc, 
                "NPC Gun",
                this.bulletPrefab,
                Random.Range(20, 80),
                Random.Range(30, 5)
            ));
        } else {
            npc.weapons.Add(new Spin(npc, "Spin", Random.Range(15, 30)));
        }
    }
}
