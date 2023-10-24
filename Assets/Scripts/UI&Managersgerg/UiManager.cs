using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // Set up serialize fields to link in the pause and game over screens, and the health script
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Health health;
    // Create booleans to track if the game is paused or over
    private bool over = false;
    private bool paused = false;
    
    // Awake method is called when the script is loaded
    void Awake()
    {
        // Set the screens off when the game is started
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        // If health is 0, run game over method, also check game over hasnt been run before so the sound dosn't play repeatedly
        if(health.currentHealth == 0 && !over)
        {
            GameOver();
        }
        // If escape is pressed, turn the pause menu on and off, slong as the game over screen isn't on
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!paused && !over)
            {
                Pause();
            }
            else{
                Resume();
            }
        }
        // Stop time when game is paused
        if(paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // Creates a method that brings up the game over screen, swaps the matching boolean, and plays the audio clip
    public void GameOver()
    {
        over = true;
        gameOverScreen.SetActive(true);
        AudioManager.instance.PlayGameOverSound();
    }
    // Creates a method that brings up the pause screen, and swaps the matching boolean
    public void Pause()
    {
        paused = true;
        pauseScreen.SetActive(true);
    }

    // Unpause the game
    public void Resume()
    {
        paused = false;
        pauseScreen.SetActive(false);
    }
    // Reload the curent scene, restarting the game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Load the first scene, opening the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Quit application, closing the game
    public void Quit()
    {
        Application.Quit();
    }
}