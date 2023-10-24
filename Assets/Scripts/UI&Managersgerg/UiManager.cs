using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Health health;
    private bool over = false;
    
    void Awake()
    {
        gameOverScreen.SetActive(false);
    }
    
    void Update()
    {
        if(health.currentHealth == 0 && !over)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        over = true;
        gameOverScreen.SetActive(true);
        AudioManager.instance.PlayGameOverSound();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}