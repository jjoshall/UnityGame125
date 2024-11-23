using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitObstacleScript : MonoBehaviour
{     
     // If player runs into collider, restart the scene
     private void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Player"))
          {
               //unlock the cursor
               Cursor.lockState = CursorLockMode.None;
               Cursor.visible = true;
               // load the game over scene
               SceneManager.LoadScene("GameOverMenu");
          }
     }
}
