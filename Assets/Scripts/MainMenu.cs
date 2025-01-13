using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Accessing Audio Manager Script
    AudioManager audioManager;

    // Function to get the componenets of a game object with a tag "Audio" which is assigned to an audio manager object in unity
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Funtion for clicking sound effects
    public void Click()
    {
        audioManager.PlaySFX(audioManager.click);
    }

    // Function to load the game
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    // Function to load the game
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    // Function to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
