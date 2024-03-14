using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerData
{
    public EnemySpawner spawner;
    public bool canSpawn;
    public float distanceToPlayer;
    public int currentNumEnemies;
    public int maxEnemies;
}

public class EnemySpawnController : MonoBehaviour
{
    [Header("Settings")]
    public float activationDistance = 20f; // Distance to activate spawners

    private List<EnemySpawner> spawners = new List<EnemySpawner>();

    [SerializeField]
    public Dictionary<EnemySpawner, SpawnerData> spawnerDataMap = new Dictionary<EnemySpawner, SpawnerData>();

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        EnemySpawner[] allSpawners = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in allSpawners)
        {
            spawners.Add(spawner);
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
            spawnerDataMap.Add(spawner, spawnerData); // Add to the dictionary
        }
    }

    void Update()
    {
        foreach (EnemySpawner spawner in spawners)
        {
            // Update distance to player and canSpawn status
            spawner.UpdateObserversSpawnerData(player, activationDistance);

            // Access the spawner's variables directly from the dictionary
            Debug.Log("Spawner: " + spawner.name);
            Debug.Log("Distance to Player: " + spawnerDataMap[spawner].distanceToPlayer);
            Debug.Log("Can Spawn: " + spawnerDataMap[spawner].canSpawn);
            Debug.Log("Current Enemies: " + spawnerDataMap[spawner].currentNumEnemies);
            Debug.Log("Max Enemies: " + spawnerDataMap[spawner].maxEnemies);
        }
    }

    // ISpawnObserver implementation
    public void OnEnemySpawned(EnemySpawner spawner, GameObject enemy)
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
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.UnregisterObserver(this);
        }
    }
}
