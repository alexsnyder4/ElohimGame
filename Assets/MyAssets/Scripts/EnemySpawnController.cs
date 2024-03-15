using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SpawnerData
{
    public EnemySpawner spawner;
    public bool canSpawn;
    public float distanceToPlayer;
    public int currentNumEnemies;
    public int maxEnemies;

    public EnemySpawner GetSpawner()
    {
        return spawner;
    }
}

public class EnemySpawnController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private List<SpawnerData> spawnerDataList = new List<SpawnerData>();

    public Dictionary<EnemySpawner, SpawnerData> spawnerDataMap = new Dictionary<EnemySpawner, SpawnerData>();

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        EnemySpawner[] allSpawners = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in allSpawners)
        {
            Debug.Log(spawner);
        }
        foreach (EnemySpawner spawner in allSpawners)
        {
            spawner.RegisterObserver(this); // Register as observer for each spawner
            // Create SpawnerData object for each spawner
            SpawnerData spawnerData = new SpawnerData
            {
                spawner = spawner,
                currentNumEnemies = spawner.currentEnemies,
                maxEnemies = spawner.maxEnemies,
                distanceToPlayer = Mathf.Infinity, // Initialize with a large value
                canSpawn = false // Initialize to false
            };
            spawnerDataList.Add(spawnerData); // Add to List to display data 
            spawnerDataMap.Add(spawner, spawnerData); // Add to the dictionary
        }
    }

    void Update()
    {
        foreach (SpawnerData spawnerData in spawnerDataList)
        {
            EnemySpawner spawner = spawnerData.GetSpawner();
            IEnumerable<object> spawnerDataPackage = spawner.GetSpawnerData();
            int currentEnemies = (int)spawnerDataPackage.ElementAt(0);
            int maxEnemies = (int)spawnerDataPackage.ElementAt(1);
            Vector3 spawnerPosition = (Vector3)spawnerDataPackage.ElementAt(2);
            float spawnerActivationDistance = (float)spawnerDataPackage.ElementAt(3);
            // Calculate the distance between the player and the spawner
            float distance = Vector3.Distance(player.position, spawnerPosition);
            // Update the canSpawn status based on the calculated distance
            bool canSpawn = distance <= spawnerActivationDistance;
            // Update the corresponding SpawnerData object
            UpdateSpawnerData(spawner, currentEnemies, maxEnemies, distance, canSpawn);
            if(canSpawn)
            {
                spawner.EnableSpawning();
                //update spawner data with distance, activation distance, and canspawn
            }
            else
            {
                spawner.DisableSpawning();
            }
        }
    }

    // ISpawnObserver implementation
    public void OnEnemySpawned(EnemySpawner spawner)
    {
        // Update the corresponding SpawnerData object when an enemy is spawned
        if (spawnerDataMap.ContainsKey(spawner))
        {
            spawnerDataMap[spawner].currentNumEnemies++;
        }
    }

    // Unregister as observer when destroyed
    private void OnDestroy()
    {
        foreach (SpawnerData spawnerData in spawnerDataList)
        {
            EnemySpawner spawner = spawnerData.GetSpawner();
            spawner.UnregisterObserver(this);
        }
    }

    public void UpdateSpawnerData(EnemySpawner spawner, int currEnemies, int maxEnemies, float distance, bool canSpawn)
    {
        // Check if the spawner exists in the dictionary
        if (spawnerDataMap.ContainsKey(spawner))
        {
            // Retrieve the SpawnerData object associated with the spawner
            SpawnerData spawnerData = spawnerDataMap[spawner];
            
            // Update the fields of the SpawnerData object
            spawnerData.currentNumEnemies = currEnemies;
            spawnerData.maxEnemies = maxEnemies;
            spawnerData.distanceToPlayer = distance;
            spawnerData.canSpawn = canSpawn;
            
            // Update the dictionary entry with the modified SpawnerData object
            spawnerDataMap[spawner] = spawnerData;
        }
    }
}
