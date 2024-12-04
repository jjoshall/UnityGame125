using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitObstacleScript : MonoBehaviour
{
     private GameOverScript gameOverUI; // Reference to the GameOverUI script
     public ParticleSystem playerExplosion;  // Reference the player's particle effects
     private GameObject player;    // Reference the Player Object

     // Making audio sounds
     public AudioClip hitSound;
     private AudioSource audioSource;

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

          // Find the player object in the scene with the tag "Player"
          player = GameObject.FindGameObjectWithTag("Player");

          // Find the audio source in the scene
          audioSource = GetComponent<AudioSource>();
          if (audioSource == null)
          {
               audioSource = gameObject.AddComponent<AudioSource>();
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
                    // Raise the position of the particle effects to avoid clipping through the ground
                    Vector3 pos = playerExplosion.transform.position;
                    pos.y += 1.5f;
                    playerExplosion.transform.position = pos;
                    playerExplosion.Play();
               }

               // Play the hit sound
               if (audioSource != null && hitSound != null)
               {
                    audioSource.PlayOneShot(hitSound);
               }

               // Deactivate player object
               player.SetActive(false);

               //unlock the cursor
               Cursor.lockState = CursorLockMode.None;
               Cursor.visible = true;

               // Show the Game Over menu after 0.25 seconds
               if (gameOverUI != null)
               {
                    StartCoroutine(ShowGameOverAfterDelay(0.3f));
               }
          }
     }

     private IEnumerator ShowGameOverAfterDelay(float delay)
     {
          yield return new WaitForSeconds(delay);
          gameOverUI.ShowGameOverMenu();
     }
}
