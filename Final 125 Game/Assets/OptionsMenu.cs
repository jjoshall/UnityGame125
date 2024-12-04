using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
     public void HandleInputData(int val)
     {
          Screen.fullScreen = (val == 1);
     }

     public void SetQuality(int qualityIndex)
     {
          QualitySettings.SetQualityLevel(qualityIndex);
     }

     public void BackButton()
     {
          SceneManager.LoadScene("MainMenu");
     }
}
