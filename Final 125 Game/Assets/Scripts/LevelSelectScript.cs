using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour
{
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
}
