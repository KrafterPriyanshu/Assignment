using UnityEngine;
using UnityEngine.UI;  // For UI components

public class ToggleActiveStateButton : MonoBehaviour
{
    // Reference to the GameObject you want to toggle
    public GameObject targetObject;

    // Method to toggle the active state of the target object
    public void ToggleActiveState()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
        else
        {
            Debug.LogError("Target object is not assigned in the Inspector!");
        }
    }
}
