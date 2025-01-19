using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning Enemies")]
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float initialSpawnRate = 4f;
    [SerializeField] private float spawnRateDecrease = 0.1f;
    [SerializeField] private float minimumSpawnRate = 0.5f;
    [SerializeField] private float difficultyIncreaseInterval = 15f;
    private float spawnRate;
    private float nextSpawnTime;
    private float difficultyTimer;
    private List<int> spawnWeights;

    [Header("Enemies")]
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField] private int easyEnemyIndex = 2;

    private void Start()
    {
        spawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + spawnRate;
        difficultyTimer = Time.time + difficultyIncreaseInterval;

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
        int selectedIndex = SelectEnemyIndex();

        if (selectedIndex == easyEnemyIndex)
        {
            int herdSize = Random.Range(2, 5);
            for (int i = 0; i < herdSize; i++)
            {
                Vector3 spawnPosition = GenerateRandomPositionWithinRadius();
                Instantiate(enemyPrefabs[selectedIndex], spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            Vector3 spawnPosition = GenerateRandomPositionWithinRadius();
            Instantiate(enemyPrefabs[selectedIndex], spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GenerateRandomPositionWithinRadius()
    {
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        return new Vector3(randomPoint.x, randomPoint.y, 0) + transform.position;
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
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
