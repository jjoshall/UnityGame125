using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour
{
    [Header("UI Elements")]
    public Image[] Level1Stars;
    public Image[] Level2Stars;
    public Image[] Level3Stars;

    [Header("Star Settings")]
    public int totalCollectables = 2; // Total number of collectables per the level
    public Sprite starSprite;
    public Sprite emptyStarSprite;

    [Header("Level Buttons")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    [Header("Star Requirement Texts")]
    public TextMeshProUGUI level2RequirementText;
    public TextMeshProUGUI level3RequirementText;

    private void Start()
    {
        // Update star display for each level
        DisplayStars(1, Level1Stars);
        DisplayStars(2, Level2Stars);
        DisplayStars(3, Level3Stars);
        // Check star requirements for unlocking levels
        UpdateLevelUnlocks();

    }
    private void UpdateLevelUnlocks()
    {
        // Calculate total stars collected across all levels
        int totalStars = 0;
        totalStars += PlayerPrefs.GetInt("Level1Stars", 0);
        totalStars += PlayerPrefs.GetInt("Level2Stars", 0);
        totalStars += PlayerPrefs.GetInt("Level3Stars", 0);

        // Unlock levels based on total stars
        level1Button.interactable = true; // Level 1 is always unlocked
        level2Button.interactable = totalStars >= 1; // Unlock Level 2 at 1 star
        level3Button.interactable = totalStars >= 3; // Unlock Level 3 at 3 stars

        // Update requirement text
        level2RequirementText.text = totalStars >= 1
            ? "Level 2"
            : "Requires 1 star to unlock";
        level3RequirementText.text = totalStars >= 3
            ? "Level 3"
            : "Requires 3 stars to unlock";

    }

    public void GoToLevel1()
     {
          // Load the first level
          SceneManager.LoadScene("Level1");
     }

     public void GoToLevel2()
     {
          // Load the second level
          SceneManager.LoadScene("Level2");
     }

     public void GoToLevel3()
     {
          // Load the third level
          SceneManager.LoadScene("Level3");
     }

     public void GoToMainMenu()
     {
          // Load the main menu scene
          SceneManager.LoadScene("MainMenu");
     }

    // Called when the player wins the game

    private void DisplayStars(int level, Image[] starImages)
    {
        // Get saved stars for the level
        int starsEarned = PlayerPrefs.GetInt($"Level{level}Stars", 0);

        // Update UI
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = (i < starsEarned) ? starSprite : emptyStarSprite;
        }
    }
}
