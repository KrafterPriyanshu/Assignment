using UnityEngine;
using UnityEngine.UI; // Import UI functionality
using UnityEngine.SceneManagement; // Import for scene management (e.g., reloading the scene)
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    private int currentHealth; // Current health of the player
    public TMP_Text healthText; // Reference to the UI Text element to display health
    public GameObject gameOverScreen; // Reference to the Game Over screen UI (set this up in your scene)

    void Start()
    {
        currentHealth = maxHealth; // Set the player's health to max at the start
        UpdateHealthUI(); // Update health UI at the start

        // Make the game over screen invisible at the start
        gameOverScreen.SetActive(false);
    }

    // Method to deal damage to the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by damage amount

        if (currentHealth < 0)
        {
            currentHealth = 0; // Ensure health does not go below 0
        }

        UpdateHealthUI(); // Update the UI after taking damage

        // If health is 0, trigger player death
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Update the health UI display
    void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth;
    }

    // Handle player death (game over logic)
    void Die()
    {
        // Display the Game Over screen
        gameOverScreen.SetActive(true);

        // Pause the game (stop time or interactions)
        Time.timeScale = 0f; // This stops the game

        // Optional: You could also display the final score or any other details
        // For example: finalScoreText.text = "Score: " + score;
        Debug.Log("Game Over!");
        Destroy(gameObject);
    }

    // Optionally, you can add a method to restart the game when the player presses a button on the game over screen
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset the game time scale (unpause the game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
