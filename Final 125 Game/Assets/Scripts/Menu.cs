using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(0);
    }

    public void GoToNextLevel()
    {
          // Load a specific scene
          SceneManager.LoadScene("Level1");
    }

     public void QuitGame()
    {
        Application.Quit();
    }
}
