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
        
    }

    // Called when the player wins the game
    
    private string GenerateStarDisplay(int collected)
    {
        Level1Stars[0].sprite = starSprite;

        string result = "Level Completed\n";

        for (int i = 0; i < totalCollectables; i++)
        {
            if (i < collected)
            {
                result += "Crystal Collected\n"; // Star earned
                Level1Stars[i + 1].sprite = starSprite;
            }
            else
            {
                result += "Missing Crystal\n"; // Empty star
            }
        }

        return result;
    }
}
