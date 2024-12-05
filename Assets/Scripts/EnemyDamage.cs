using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 30;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f; // Cooldown time between attacks
    private float lastAttackTime = 0f;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        // Only attack if enough time has passed since the last attack
        if (Vector3.Distance(transform.position, playerHealth.transform.position) <= attackRange)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                playerHealth.TakeDamage(damageAmount);
                lastAttackTime = Time.time; // Reset the cooldown timer
                Destroy(gameObject);
            }
        }
    }
}

