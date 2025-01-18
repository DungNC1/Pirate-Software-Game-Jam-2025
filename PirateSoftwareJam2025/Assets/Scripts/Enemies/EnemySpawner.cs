using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;  // Different types of enemies
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float initialSpawnRate = 2f;
    [SerializeField] private float spawnRateDecrease = 0.1f;
    [SerializeField] private float minimumSpawnRate = 0.5f;
    [SerializeField] private float difficultyIncreaseInterval = 30f;  // Time interval to increase difficulty
    private float spawnRate;
    private float nextSpawnTime;
    private float difficultyTimer;
    private List<int> spawnWeights;

    private void Start()
    {
        spawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + spawnRate;
        difficultyTimer = Time.time + difficultyIncreaseInterval;

        // Initialize spawn weights
        spawnWeights = new List<int>();
        foreach (var enemy in enemyPrefabs)
        {
            spawnWeights.Add(1);
        }
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            AdjustDifficulty();
            nextSpawnTime = Time.time + spawnRate;
        }

        if (Time.time >= difficultyTimer)
        {
            IncreaseDifficulty();
            difficultyTimer = Time.time + difficultyIncreaseInterval;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
        int selectedIndex = SelectEnemyIndex();
        Instantiate(enemyPrefabs[selectedIndex], spawnPosition, Quaternion.identity);
    }

    private int SelectEnemyIndex()
    {
        int totalWeight = 0;
        foreach (var weight in spawnWeights)
        {
            totalWeight += weight;
        }

        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int i = 0; i < spawnWeights.Count; i++)
        {
            cumulativeWeight += spawnWeights[i];
            if (randomValue < cumulativeWeight)
            {
                return i;
            }
        }

        return spawnWeights.Count - 1; 
    }

    private void AdjustDifficulty()
    {
        if (spawnRate > minimumSpawnRate)
        {
            spawnRate -= spawnRateDecrease;
        }
    }

    private void IncreaseDifficulty()
    {
        for (int i = 0; i < spawnWeights.Count; i++)
        {
            if (spawnWeights[i] > 0)
            {
                spawnWeights[i]--;
            }
        }

        if (spawnWeights[spawnWeights.Count - 1] < 3)
        {
            spawnWeights[spawnWeights.Count - 1]++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);  // Drawing spawn radius
    }
}