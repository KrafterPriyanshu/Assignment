using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;  // Health value for the enemy
    public GameObject deathEffect; // Optional death effect (particles, etc.)

    private KillCounter killCounter;

    private void Start()
    {
        // Find the KillCounter script in the scene
        killCounter = FindObjectOfType<KillCounter>();
    }

    // Apply damage to the enemy
    public void ApplyDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    // Handle the enemy's death
    private void Die()
    {
        if (killCounter != null)
        {
            killCounter.IncrementKillCount(); // Increment kill count when this enemy dies
        }

        // Optional: instantiate death effect, such as explosion
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // Destroy the enemy
    }
}
