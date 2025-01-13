using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorMechanics : MonoBehaviour
{
    // Variables that can be accessed outside the script 
    public int gemsNeeded;
    public bool isOpen;
    public string nextScene;

    public void OpenDoor(GameObject obj)
    {
        // Access the player script to check how many gems the player has collected
        PlayerController manager = obj.GetComponent<PlayerController>();

        // If Player collected is equal to the gems required to move into the door
        // the player can proceed to the next level
        if (manager != null)
        {
            if (manager.gems == gemsNeeded)
            {

                ProceedToNextLevel();

            }
        }
    }

    // Function to move to the next level
    void ProceedToNextLevel()
    {
        
        SceneManager.LoadScene(nextScene);
        

    }
}
