using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // Reference to the player's transform
    public Vector3 offset;     // The offset between the camera and the player
    public float smoothSpeed = 0.125f; // Smoothness factor for the camera movement

    void LateUpdate()
    {
        // Calculate the desired position by adding the offset to the player's position
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
