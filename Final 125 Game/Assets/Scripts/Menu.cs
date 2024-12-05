using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToNextLevel()
    {
          // Load a specific scene
          SceneManager.LoadScene("LevelSelect");
    }

     public void QuitGame()
    {
        Application.Quit();
    }
}
