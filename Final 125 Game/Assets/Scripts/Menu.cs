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

    public void NewGame()
    {
        // Reset saved stars for all levels
        PlayerPrefs.SetInt("Level1Stars", 0);
        PlayerPrefs.SetInt("Level2Stars", 0);
        PlayerPrefs.SetInt("Level3Stars", 0);
        PlayerPrefs.Save();

        // Navigate to the Level Select screen
        SceneManager.LoadScene("LevelSelect");
    }
}
