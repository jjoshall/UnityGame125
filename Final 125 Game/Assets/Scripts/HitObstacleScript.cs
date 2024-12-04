using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitObstacleScript : MonoBehaviour
{
     private GameOverScript gameOverUI; // Reference to the GameOverUI script
     public ParticleSystem playerExplosion;  // Reference the player's particle effects

     private void Start()
     {
          // Find player's particle effects in the scene
          playerExplosion = GameObject.Find("PlayerExplosion").GetComponent<ParticleSystem>();

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
               // Play the particle effects
               if (playerExplosion != null)
               {
                    playerExplosion.transform.position = other.transform.position;
                    playerExplosion.Play();
               }

               //unlock the cursor
               Cursor.lockState = CursorLockMode.None;
               Cursor.visible = true;

               // Show the Game Over menu after 0.25 seconds
               if (gameOverUI != null)
               {
                    StartCoroutine(ShowGameOverAfterDelay(0.25f));
               }
          }
     }

     private IEnumerator ShowGameOverAfterDelay(float delay)
     {
          yield return new WaitForSeconds(delay);
          gameOverUI.ShowGameOverMenu();
     }
}
