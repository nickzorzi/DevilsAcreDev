using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float initialTimeBetweenSpawns;
    public float timeIntervalToDecreaseSpawnTime;
    private float nextSpawnTime;
    private float currentTimeBetweenSpawns;
    public GameObject enemy;
    public Transform[] spawnPoints;
    public float spawnRateIncreaseInterval;
    public float spawnRateIncreasePercentage;

    private bool hasStartedSpawning = false;

    void Start()
    {
        nextSpawnTime = Time.time;
        currentTimeBetweenSpawns = initialTimeBetweenSpawns;
    }

    void Update()
    {
        if (Time.time >= 3f && !hasStartedSpawning)
        {
            hasStartedSpawning = true;
            nextSpawnTime = Time.time + currentTimeBetweenSpawns;
        }

        if (spawnPoints.Length > 0 && Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + currentTimeBetweenSpawns;
            SpawnEnemy();

            // Check for spawn rate increase
            if (Time.time >= spawnRateIncreaseInterval)
            {
                currentTimeBetweenSpawns *= (1 - spawnRateIncreasePercentage); // Decrease spawn time
                spawnRateIncreaseInterval += spawnRateIncreaseInterval; // Increase the threshold for spawn rate increase
            }
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomIndex];
        Instantiate(enemy, randomSpawnPoint.position, Quaternion.identity);
    }
}
