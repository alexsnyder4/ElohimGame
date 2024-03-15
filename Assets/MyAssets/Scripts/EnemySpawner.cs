using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    public float activationDistance = 160f;
    public GameObject[] enemyPrefabs;

    [Header("Spawn Distribution")]
    public bool randomSpawnRate = true;
    public float[] spawnRateWeights;

    public bool canSpawn = false;
    public int maxEnemies = 5;
    public int currentEnemies = 0;

    private GameObject enemyPrefab;
    private BoxCollider spawnCollider;
    private List<EnemySpawnController> observers = new List<EnemySpawnController>();
    private System.Random random;

    void Start()
    {
        spawnCollider = GetComponent<BoxCollider>();
        random = new System.Random(); // Initialize random instance
        InvokeRepeating("SpawnEnemy", 0f, 3f); // Changed spawnInterval to 3 seconds
    }

    void SpawnEnemy()
    {
        if (!canSpawn || currentEnemies >= maxEnemies || enemyPrefabs.Length == 0)
            return;

        // Randomly select an enemy prefab based on spawn rate weights
        enemyPrefab = SelectRandomEnemyPrefab(randomSpawnRate);
        
        // Spawn the selected enemy prefab
        Vector3 spawnPosition = GetRandomPointInBounds(spawnCollider.bounds);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemies++;

        // Notify observers
        foreach (EnemySpawnController observer in observers)
        {
            observer.OnEnemySpawned(this);
        }
    }

    
    private GameObject SelectRandomEnemyPrefab(bool isSpawningRandomly)
    {
        if(isSpawningRandomly) //choose random enemy prefab
        {
            var randindex = random.Next(0,enemyPrefabs.Length);
            return enemyPrefabs[randindex];
        }
        else //choose random enemy prefab with weights
        {
            float totalWeight = 0f;
            foreach (float weight in spawnRateWeights)
            {
                totalWeight += weight;
            }

            float randomValue = (float)random.NextDouble() * totalWeight; // Use System.Random
            float weightSum = 0f;

            for (int i = 0; i < enemyPrefabs.Length; i++)
            {
                weightSum += spawnRateWeights[i];
                if (randomValue <= weightSum)
                {
                    return enemyPrefabs[i];
                }
            }
        }

        // This should never happen, but just in case
        return enemyPrefabs[enemyPrefabs.Length - 1];
    }

    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);

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

    public Vector3 GetSpawnerPosition()
    {
        return transform.position;
    }

    public float GetActivationDistance()
    {
        return activationDistance;
    }

    public IEnumerable<object> GetSpawnerData()
    {
        yield return currentEnemies;
        yield return maxEnemies;
        yield return transform.position;
        yield return activationDistance;
    }
}