using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; // Import for UI functionality
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject enemyPrefab; // Enemy prefab
    public float spawnRadius = 20f; // Radius around the player where enemies spawn
    public float spawnInterval = 1f; // Time interval between spawning each enemy
    public PlayerController playerController; // Reference to PlayerController to notify on kills
    public TMP_Text levelText; // UI Text element to display current level

    private List<GameObject> enemies = new List<GameObject>(); // Track active enemies
    private int currentWave = 1; // Current wave number
    private int enemiesToSpawnInWave = 100; // Initial number of enemies for the first wave

    void Start()
    {
        // Initialize the wave system
        UpdateLevelText();

        // Start the spawning coroutine
        StartCoroutine(SpawnEnemiesForWave());

        // Start continuous checks to maintain the fixed count
        StartCoroutine(MaintainEnemyCount());
    }

    void Update()
    {
        // Update destinations for all active enemies
        foreach (var enemy in enemies)
        {
            if (enemy != null && player != null)
            {
                var agent = enemy.GetComponent<NavMeshAgent>();
                agent.SetDestination(player.position);
            }
        }
    }

    IEnumerator SpawnEnemiesForWave()
    {
        for (int i = 0; i < enemiesToSpawnInWave; i++)
        {
            // Generate random spawn position around the player
            Vector3 randomPosition = player.position + Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 0; // Keep enemies on the ground

            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            // Ensure the enemy has a NavMeshAgent
            if (enemy.GetComponent<NavMeshAgent>() == null)
            {
                enemy.AddComponent<NavMeshAgent>();
            }

            // Attach the enemy's health or destruction logic, including handling death
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth == null)
            {
                enemyHealth = enemy.AddComponent<EnemyHealth>();
            }

            // Add to the list of active enemies
            enemies.Add(enemy);

            // Wait for the spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MaintainEnemyCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Remove null references (destroyed enemies)
            enemies.RemoveAll(enemy => enemy == null);

            // Check if all enemies for the current wave are dead
            if (enemies.Count == 0)
            {
                // Proceed to the next wave
                currentWave++;
                UpdateLevelText();
                enemiesToSpawnInWave = currentWave * 100; // Increase enemy count per wave (10 enemies per wave)
                StartCoroutine(SpawnEnemiesForWave()); // Start spawning enemies for the new wave
            }
        }
    }

    // Update the level text to show the current wave
    void UpdateLevelText()
    {
        levelText.text = "Level: " + currentWave;
    }

    // Call this method when an enemy is killed
    void OnEnemyDeath(GameObject enemy)
    {
        // Notify the PlayerController of the kill
        if (playerController != null)
        {
            playerController.OnEnemyKilled();
        }

        // Remove the dead enemy from the list and destroy the GameObject
        enemies.Remove(enemy);
        Destroy(enemy);
    }
}
