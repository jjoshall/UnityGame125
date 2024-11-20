using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameHitboxScript : MonoBehaviour
{
     // If the player enters the hitbox, say they beat the level and restart the game.  
     private void OnTriggerEnter(Collider other)
     {
          if (other.gameObject.tag == "Player")
          {
               Debug.Log("You beat the level! Restarting game in 3 seconds...");
               // Wait for 3 seconds before restarting the game  
               StartCoroutine(RestartGame());
          }
     }

     IEnumerator RestartGame()
     {
          yield return new WaitForSeconds(3);
          Scene scene = SceneManager.GetActiveScene();
          SceneManager.LoadScene(scene.name);
     }
}
