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
               // Restart the scene
               Scene scene = SceneManager.GetActiveScene();
               SceneManager.LoadScene(scene.name);
          }
     }
}
