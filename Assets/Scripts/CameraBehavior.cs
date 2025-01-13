using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // Variables that can be accessed outside the script
    public Transform player; // Reference to the player's Transform component for tracking their position
    public float smoothSpeed = 0.25f; // Controls the smoothing speed for the camera movement

    // Private variables used internally by the script
    private Vector3 offset = new Vector3(0f, 0f, -10f); // Offset to maintain a fixed distance between the camera and the player
    private Vector3 velocity = Vector3.zero; // Tracks the current velocity of the camera for SmoothDamp

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired position of the camera based on the player's position and the offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }

}
