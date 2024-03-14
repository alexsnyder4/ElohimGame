using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public bool canSpawn = false;
    public int maxEnemies = 5; 

    public int currentEnemies = 0; 
    public float distanceToPlayer;
    private BoxCollider spawnCollider; 
    private List<EnemySpawnController> observers = new List<EnemySpawnController>();

    void Start()
    {
        spawnCollider = GetComponent<BoxCollider>();
        InvokeRepeating("SpawnEnemy", 0f, 3f); // Changed spawnInterval to 3 seconds
    }

    void SpawnEnemy()
    {
        if(!canSpawn)
            return;
        if (currentEnemies >= maxEnemies)
            return;

        Vector3 spawnPosition = GetRandomPointInBounds(spawnCollider.bounds);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemies++;

        // Notify observers
        foreach (EnemySpawnController observer in observers)
        {
            observer.OnEnemySpawned(this, enemy);
        }
    }

    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    // Method for observer management
    public void RegisterObserver(EnemySpawnController observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(EnemySpawnController observer)
    {
        observers.Remove(observer);
    }

    public void DisableSpawning()
    {
        canSpawn = false;
    }

    public void EnableSpawning()
    {
        canSpawn = true;
    }

    public void UpdateObserversSpawnerData(Transform playerTransform, float activationDistance)
    {
        distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        canSpawn = GetSpawnStatus();
    }

    public bool GetSpawnStatus()
    {
        return canSpawn;
    }
}
