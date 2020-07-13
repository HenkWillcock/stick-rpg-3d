using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject NPCprefab;
    public GameObject carPrefab;
    public GameObject helicopterPrefab;

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

        npc.name = this.getNPCName();
        npc.healthRegen = Random.Range(0, 2);
        npc.updateMaxHealth(Random.Range(50, 300));

        List<Item> allItems = new List<Item>();
        allItems.Add(Spin.BASIC_SPIN());
        allItems.Add(Spin.SUPER_SPIN());
        allItems.Add(Gun.PISTOL());
        allItems.Add(Gun.MACHINE_PISTOL());
        allItems.Add(Gun.ASSAULT_RIFLE());
        allItems.Add(Gun.SNIPER());
        allItems.Add(Gun.HEAVY_SNIPER());
        allItems.Add(Shotgun.SHOTGUN());
        allItems.Add(Shotgun.DOUBLE_SHOTGUN());
        allItems.Add(Shotgun.AUTO_SHOTGUN());

        foreach(Item item in allItems) {
            if (item.chanceNPCHas > Random.Range(0f, 1f)) {
                npc.inventory.AddItem(item);  // TODO clone rather than adding another reference
            }
        }
    }

    private string getNPCName() {
        // TODO seems like 2 names end up way more common than the others.
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
            "Sarah",
            "Baldrick",
            "Pieterson",
            "Hans",
            "Jeremy",
            "Mark",
            "Tony",
            "Neddard",
            "Chris-R",
            "Johnny",
            "Tommy Wiseau",
            "Greg",
            "Davis",
            "Javis",
            "Pavis",
            "Gobby",
            "Declan",
            "Derek",
            "Smitty",
            "Big T",
            "Lisa",
            "Damo",
            "Sammy",
            "Jacky",
            "Bobby",
            "Robby",
            "Roberto"
        };
        int index = System.Convert.ToInt32(Random.Range(0, names.Count));
        return names[index];
    }
}
