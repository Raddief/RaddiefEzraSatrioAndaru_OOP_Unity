using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 6;
    public int totalKill = 0;
    private int totalKillWave = 3;


    [SerializeField] private float spawnInterval = 3f;


    [Header("Spawned Enemies Counter")]
    public int spawnCount = 2;
    public int defaultSpawnCount = 2;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;


    public CombatManager combatManager;


    public bool isSpawning = false;

    private float timer;

    void Start()
    {
        // Initialize the timer
        timer = spawnInterval;
    }

    void Update()
    {
        // Check if the spawner is active
        if (isSpawning)
        {
            // Decrease the timer
            timer -= Time.deltaTime;

            // Check if the timer has reached zero
            if (timer <= 0)
            {
                // Reset the timer
                timer = spawnInterval;

                // Spawn enemies
                SpawnEnemies();
            }
        }
    }

    void SpawnEnemies()
    {
        // Loop through the number of enemies to spawn
        for (int i = 0; i < spawnCount; i++)
        {
            // Instantiate the enemy prefab
            Instantiate(spawnedEnemy, transform.position, transform.rotation);
        }
    }

    // Function to increase the spawn count
    public void IncreaseSpawnCount()
    {
        // Check if the total kills meet the minimum requirement
        if (totalKill >= minimumKillsToIncreaseSpawnCount)
        {
            // Increase the spawn count multiplier
            spawnCountMultiplier++;

            // Update the spawn count
            spawnCount = defaultSpawnCount * spawnCountMultiplier;

            // Reset the total kill count
            totalKill = 0;
        }
    }

    // Function to handle enemy death
    public void EnemyDied()
    {
        // Increase the total kill count
        totalKill++;

        // Increase the spawn count if necessary
        IncreaseSpawnCount();
    }
}