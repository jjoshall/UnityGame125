using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitObstacleScript : MonoBehaviour
{
    private GameOverScript gameOverUI; // Reference to the GameOverUI script
    private void Start()
    {
        // Find the GameOverUI script in the scene
        gameOverUI = FindObjectOfType<GameOverScript>();
        if (gameOverUI == null)
        {
            Debug.LogError("GameOverUI not found in the scene! Ensure it exists and is active.");
        }
    }

    // If player runs into collider, restart the scene
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Show the Game Over menu
            if (gameOverUI != null)
            {
                gameOverUI.ShowGameOverMenu();
            }
        }
    }
}
