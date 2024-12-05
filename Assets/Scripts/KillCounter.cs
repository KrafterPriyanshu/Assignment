using UnityEngine;
using TMPro;  // Required for TextMesh Pro UI elements

public class KillCounter : MonoBehaviour
{
    public TMP_Text killCountText;  // Reference to the TextMesh Pro UI element
    private int killCount = 0;  // Variable to store the number of kills

    // This method will be called from the EnemyHealth script or when an enemy dies
    public void IncrementKillCount()
    {
        killCount++;  // Increase the kill count
        UpdateKillCountUI();  // Update the UI text
    }

    // Update the kill count text on screen
    private void UpdateKillCountUI()
    {
        killCountText.text = "Kills: " + killCount.ToString();  // Update text to show current kill count
    }
}
