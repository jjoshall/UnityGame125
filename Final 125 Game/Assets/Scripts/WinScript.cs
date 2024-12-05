using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinScript : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject winScreenCanvas; // Assign this in the Inspector
    public TextMeshProUGUI starText;           // UI text displaying the stars
    public Image[] stars;
    

    [Header("Star Settings")]
    public int totalCollectables = 2; // Total number of collectables in the level
    public Sprite starSprite;
    public Sprite emptyStarSprite;

    public PlayerGen playerGen;

    private void Start()
    {
        if (winScreenCanvas != null)
        {
            winScreenCanvas.SetActive(false); // Ensure the panel is hidden at the start
        }
    }

    // Called when the player wins the game
    public void ShowWinScreen()
    {
        if (winScreenCanvas != null)
        {
            winScreenCanvas.SetActive(true);
        }

        int collected = playerGen.GetCollectableCount();
        if (starText != null)
        {
            starText.text = GenerateStarDisplay(collected);
            playerGen.SaveStars(SceneManager.GetActiveScene().buildIndex - 1, collected + 1);
        }

        Time.timeScale = 0f; // Pause the game
    }

    private string GenerateStarDisplay(int collected)
    {
        stars[0].sprite = starSprite;

        string result = "Level Completed\n";

        for (int i = 0; i < totalCollectables; i++)
        {
            if (i < collected)
            {
                result += "Crystal Collected\n"; // Star earned
                stars[i+1].sprite = starSprite;
            }
            else
            {
                result += "Missing Crystal\n"; // Empty star
            }
        }

        return result;
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
