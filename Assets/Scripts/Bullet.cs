using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f; // Time before bullet auto-destroys if no collision occurs
    public float damage = 10f; // Amount of damage the bullet deals

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after a certain time to avoid clutter
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hits an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Apply damage to the enemy (if it has an EnemyHealth component)
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(damage); // Deal damage to the enemy
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
