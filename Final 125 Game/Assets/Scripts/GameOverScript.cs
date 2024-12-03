using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverCanvas; // Assign this in the Inspector

    private void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Ensure the panel is hidden at the start
        }
    }

    // Called when the player loses the game
    public void ShowGameOverMenu()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }

    // Button functionality for retry
    public void Retry()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    // Button functionality for main menu
    public void MainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu"); // Replace with your Main Menu scene name
    }
}
