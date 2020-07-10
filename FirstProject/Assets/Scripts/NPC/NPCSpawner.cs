using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject NPCprefab;

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

        npc.name = this.getNPCName();
        npc.healthRegen = Random.Range(0, 2);
        npc.updateMaxHealth(Random.Range(50, 300));

        if (Random.Range(0f, 1f) > 0.5f) {
            npc.inventory.items.Add(Gun.PISTOL);
        } else {
            npc.inventory.items.Add(new Spin("Spin", Random.Range(15, 30)));
        }
    }

    private string getNPCName() {
        List<string> names = new List<string>{
            "Pete",
            "Douglas",
            "Bob",
            "Tucker",
            "Gerald",
            "Fredrick",
            "Steven",
            "Sian",
            "Dave",
            "Sarah"
        };
        int index = new System.Random().Next(names.Count);
        return names[index];
    }
}
