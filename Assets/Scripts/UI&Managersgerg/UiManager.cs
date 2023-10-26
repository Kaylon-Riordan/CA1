using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// code in this script is based off code from this video (Pandemonium (2022). Unity 2D Platformer for Complete Beginners - #14 GAME OVER. YouTube. Available at: https://www.youtube.com/watch?v=3tQSAtaSwvc&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=15 [Accessed 25 Oct. 2023].)
public class UiManager : MonoBehaviour
{
    // Set up serialize fields to link in the pause and game over screens, and the health script
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private Health health;
    [SerializeField] private Victory victory;
    // Create booleans to track if the game is paused or over
    private bool over = false;
    private bool paused = false;
    private bool won = false;
    
    // Awake method is called when the script is loaded
    void Awake()
    {
        // Set the screens off when the game is started
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        victoryScreen.SetActive(false);
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
            if(!paused && !over && !won)
            {
                Pause();
            }
            else{
                Resume();
            }
        }
        // Stop time when game is paused
        if(paused || over || won)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        // If health is 0, run game over method, also check game over hasnt been run before so the sound dosn't play repeatedly
        if(victory.win && !won)
        {
            Victory();
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
    // This method brings up the victory screen, and plays the victory sound
    public void Victory()
    {
        won = true;
        victoryScreen.SetActive(true);
        AudioManager.instance.PlayVictorySound();
    }
}