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

    private void Start()
    {
        // Update star display for each level
        DisplayStars(1, Level1Stars);
        DisplayStars(2, Level2Stars);
        DisplayStars(3, Level3Stars);
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
