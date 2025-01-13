using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Empty Variables that requires respective components from unity editor
    [Header("AudioSource")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("AudioClip")]
    public AudioClip background;
    public AudioClip click;
    public AudioClip pickUpGem;
    public AudioClip CheckpointReached;
    public AudioClip RespawnSFX;
    public AudioClip DoorOpened;
    public AudioClip Jumped;
    public AudioClip Bat;

    // Start to play the assigned background music 
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play(); 
    }

    // Function that plays called clips only once
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
