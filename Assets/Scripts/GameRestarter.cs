using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset the game time scale (unpause the game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("MainMenu");
    }public void GameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
