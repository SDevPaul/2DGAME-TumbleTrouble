using UnityEngine;
using UnityEngine.Events;

public class Interactables : MonoBehaviour
{
    // Variables that can be accessed outside the script
    public bool isInRange; // Checks if player is in range
    public KeyCode interactKey = KeyCode.E; // Interact Key is E
    public UnityEvent interactAction; // Stores the action when interacted

    // Update is called once per frame
    void Update()
    {
        // Checks if the Player is in range then
        if (isInRange)
        {
            // check if the player interacted with the object
            if (Input.GetKeyDown(interactKey))
            {
                // if yes then call the assigned action
                interactAction.Invoke();
            }
        }
    }

    // Updates the bool isInRange to true when the player is in range 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    // Updates the bool isInRange to false when the player is out of range 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
