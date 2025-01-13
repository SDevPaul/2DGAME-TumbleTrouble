using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Accessing Audio Manager Script
    AudioManager audioManager;

    // Accessing the PlayerController Script
    PlayerController playerController;

    // Variables that can be accessed outside the script
    public Transform respawnPoint; // Variable that stores the position of the checkpoint

    private bool active = false; // A bool for checking if the checkpoint is active
    private ParticleSystem[] particleSystems; // An Array of particles from the checkpoint

    // Start is called before the first frame update
    private void Awake()
    {
        // Getting all components of its respective varibales
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the checkpoint is not active
        if (!active)
        {

            // Checks if the player touches the checkpoint
            if (collision.CompareTag("Player"))
            {
                audioManager.PlaySFX(audioManager.CheckpointReached);
                // Updates the player respawnpoint to the checkpoint
                // Light up the flame of the checkpoint to indicate active checkpoint
                playerController.updateCheckpointPos(respawnPoint.position);
                PlayAllParticles();

            }
            active = true;
        }
    }

    // Function to light up the fire of the checkpoint
    void PlayAllParticles()
    {
        // Playing all the particles to make a fire
        foreach (var particles in particleSystems)
        {
            particles.Play();
        }
    }
}
