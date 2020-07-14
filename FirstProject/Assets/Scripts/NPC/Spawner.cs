using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject NPCprefab;
    public GameObject carPrefab;
    public GameObject helicopterPrefab;

    static List<string> MALES_NAMES = new List<string>{
        "Pete",
        "Douglas",
        "Bob",
        "Tucker",
        "Gerald",
        "Fredrick",
        "Steven",
        "Dave",
        "Baldrick",
        "Pieterson",
        "Hans",
        "Jeremy",
        "Mark",
        "Tony",
        "Ned",
        "Chris-R",
        "Johnny",
        "Tommy Wiseau",
        "Greg",
        "Davis",
        "Javis",
        "Gobby",
        "Declan",
        "Derek",
        "Smitty",
        "Big T",
        "Lisa",
        "Damo",
        "Sammy",
        "Bobby",
        "Robby",
        "Roberto"
    };

    static List<string> FEMALE_NAMES = new List<string>{
        "Sian",
        "Sarah",
        "Jacky",
        "Jane",
        "Bethany",
        "Mary-Lyn",
        "Nina-Rose",
        "Anna",
        "Rose",
        "Nina",
        "Sybil",
        "Pavis",
    };

    void Start()
    {
        for (int i = 0; i <= 30; i++) {
            this.SpawnNPC();
        }
        for (int i = 0; i <= 10; i++) {
            this.SpawnVehicle();
        }
        InvokeRepeating("SpawnNPC", 0f, 10f);
    }

    public Vector3 GetRandomLocation() {
        float worldHeight = 4;
        float x = Random.Range(-50f, 50f);
        float z = Random.Range(-50f, 50f);

        return new Vector3(x, worldHeight, z);
    }

    public GameObject InstantiateAtRandomLocation(GameObject prefab) {
        return Object.Instantiate(
            prefab,
            this.GetRandomLocation(),
            new Quaternion(0, 0, 0, 0)
        );
    }

    void SpawnVehicle() {
        GameObject newVehicle = this.InstantiateAtRandomLocation(this.carPrefab);

        if (Random.Range(0f, 1f) > 0.9f) {
            Car carComponent = newVehicle.GetComponent<Car>();
            carComponent.name = "Bugatti Veyron";
            carComponent.motorTorque = 10000;
            carComponent.topRPM = 50;
            carComponent.brakeTorque = 10000;
        }
    }

    void SpawnNPC() {
        GameObject newNPC = this.InstantiateAtRandomLocation(this.NPCprefab);

        NPC npc = newNPC.GetComponent<NPC>();

        if (Random.Range(0f, 1f) > 0.5f) {
            this.GiveMaleAttributes(npc);
        } else {
            this.GiveFemaleAttributes(npc);
        }

        npc.healthRegen = Random.Range(0, 2);
        npc.updateMaxHealth(Random.Range(50, 300));

        foreach(Item item in Item.GetAllItems()) {
            if (item.chanceNPCHas > Random.Range(0f, 1f)) {
                npc.inventory.AddItem(item);
            }
        }
    }

    private string getNPCName(List<string> nameList) {
        int index = System.Convert.ToInt32(Random.Range(0, nameList.Count));
        return nameList[index];
    }

    public void GiveMaleAttributes(NPC npc) {
        npc.gender = Gender.Male;
        npc.name = this.getNPCName(Spawner.MALES_NAMES);
        npc.ChangeToColor(new Color(0.85f, 0.55f, 0f));
    }

    public void GiveFemaleAttributes(NPC npc) {
        npc.gender = Gender.Female;
        npc.name = this.getNPCName(Spawner.FEMALE_NAMES);
        npc.ChangeToColor(new Color(1f, 0.37f, 0.65f));
    }
}
