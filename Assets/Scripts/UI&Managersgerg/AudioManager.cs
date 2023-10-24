using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Creates an empty instance of this script
    public static AudioManager instance; 

    // Set public variables so sounds can be linked in engine
    [Header("Sounds")]
    public AudioClip playerJump;
    public AudioClip playerShoot;
    public AudioClip hurt;
    public AudioClip grav;
    public AudioClip gameOver;

    [Header("Music")]
    public AudioClip backgroundMusic; 

    // Set variables for audio sources
    [Header("Audio Sources")]
    private AudioSource soundEffectsSource; 
    private AudioSource backgroundMusicSource; 

    // Awake method is called when the script is loaded
    void Awake()
    {
        // Create audio sources that link to variables
        soundEffectsSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>(); 

        // This if statement follows the singleton design philosophy, so only one instance of this file can exist at a time
        // If there is no instance of this script currently set instance to be this instance of the script, set game object not to be destroyed when loading different scene
        if(instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        // If there is already an instance of this game, destroy the current one trying to be made
        else
        {
            Destroy(gameObject);
            return;
        } 
        // Set the music source to use the backgroundMusic variable, set it to loop and play it
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true; 
        backgroundMusicSource.Play();
    }

    // Set up methods to be called in other scripts, that play various sound effects
    public void PlayPlayerJumpSound()
    {
        soundEffectsSource.PlayOneShot(playerJump);
    }
    public void PlayPlayerShootSound()
    {
        soundEffectsSource.PlayOneShot(playerShoot);
    }
    public void PlayHurtSound()
    {
        soundEffectsSource.PlayOneShot(hurt);
    }
    public void PlayGravSound()
    {
        soundEffectsSource.PlayOneShot(grav);
    }
    public void PlayGameOverSound()
    {
        soundEffectsSource.PlayOneShot(gameOver);
    }
}
