using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    
    void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waveInterval)
        {
            StartNewWave();
            timer = 0;
        }
    }

    void StartNewWave()
    {
        waveNumber++;
        totalEnemies = CalculateTotalEnemiesForWave(waveNumber);

        foreach (var spawner in enemySpawners)
        {
            spawner.isSpawning = true;
            spawner.spawnCount = totalEnemies / enemySpawners.Length;
        }
    }

    int CalculateTotalEnemiesForWave(int waveNumber)
    {
        // Implement your own logic to calculate the number of enemies based on the wave number
        return waveNumber * 10; // Example: Increase the number of enemies by 10 each wave
    }
}
