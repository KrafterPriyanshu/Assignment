using UnityEngine;
using TMPro; // Ensure you have this if you're using TextMeshPro
using UnityEngine.SceneManagement; // For scene loading

public class TimerManager : MonoBehaviour
{
    public float timeLimit = 120f; // Set the time limit (e.g., 2 minutes)
    private float timeRemaining;
    public TMP_Text timerText; // Reference to the UI Text element
    public GameObject winScreen; // Reference to the Win Screen UI (you can create a Win Screen UI and disable it initially)
    public GameObject gameOverScreen; // Reference to the Game Over UI (optional, you can also handle it in the winScreen)
    private bool gameOver = false;

    void Start()
    {
        timeRemaining = timeLimit;
        winScreen.SetActive(false); // Ensure win screen is hidden initially
        gameOverScreen.SetActive(false); // Hide game over screen initially
    }

    void Update()
    {
        if (!gameOver)
        {
            // Countdown timer
            timeRemaining -= Time.deltaTime;

            // Update the timer text on the screen
            DisplayTime(timeRemaining);

            // Check if the time is up and trigger win or game over
            if (timeRemaining <= 0)
            {
                GameComplete();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Display the time on the UI (e.g., 02:30 format)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameComplete()
    {
        gameOver = true;

        // Show win screen
        winScreen.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;

        // Optionally, you can add a delay to transition to the next scene
        Invoke("LoadNextScene", 3f); // Delay for 3 seconds before loading next scene
    }

    void LoadNextScene()
    {
        // Load the next scene (you can replace this with your own logic)
        // For example, this could load the next level in your game
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); // Loads the next scene
    }

    public void LoadMainMenu()
    {
        // Load the main menu if the game is over or player opts to quit
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your main menu scene's name
    }

    // Call this method if you want to stop the timer or reset it
    public void ResetTimer()
    {
        gameOver = false;
        timeRemaining = timeLimit;
        winScreen.SetActive(false); // Hide the win screen
        gameOverScreen.SetActive(false); // Hide the game over screen

        // Resume the game (unpause it)
        Time.timeScale = 1f;
    }
}
