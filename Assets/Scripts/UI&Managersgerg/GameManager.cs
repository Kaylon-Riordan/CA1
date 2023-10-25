using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    // Awake method is called when the script is loaded
    void Awake()
    {
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
    }

    // Load the first scene, opening the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Load the second scene, opening the game level
    public void StartGame()
    {
       SceneManager.LoadScene(1); 
    }
    // Reload the curent scene, restarting the game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Quit application, closing the game
    public void Quit()
    {
        Application.Quit();
    }
}