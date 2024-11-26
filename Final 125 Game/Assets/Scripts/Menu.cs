using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private bool isPauseMenu = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.name == "PauseMenu")
        {
            isPauseMenu = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu();
            }
        }
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(0);
    }

    public void GoToNextLevel()
    {
        // Load the next scene in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TogglePauseMenu()
    {
        // Toggle the pause menu
        gameObject.SetActive(!gameObject.activeSelf);
        // unlock the cursor
        Cursor.lockState = gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = gameObject.activeSelf;
        // Pause the game
        Time.timeScale = gameObject.activeSelf ? 0 : 1;
    }
}
