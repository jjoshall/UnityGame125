using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    void Start()
    {
        pauseMenu = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        
    }

    public void TogglePauseMenu()
    {
        // Toggle the pause menu
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        // unlock the cursor
        Cursor.lockState = pauseMenu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = pauseMenu.activeSelf;
        // Pause the game
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
    }
}
