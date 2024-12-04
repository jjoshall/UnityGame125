using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableScript : MonoBehaviour
{
    [Header("Settings")]
    public int value = 1;

    [Header("References")]
    public Animator animator;
    public float destroyDelay = 0.5f;
     
    // Reference the player's particle effects
    public ParticleSystem playerExplosion;  

     // Making audio sounds
     public AudioClip collectSound;
     private AudioSource audioSource;

     private bool collected = false;

     private void Start()
     {
          // Find the audio source in the scene
          audioSource = GetComponent<AudioSource>();
          if (audioSource == null)
          {
               audioSource = gameObject.AddComponent<AudioSource>();
          }
     }

     private void OnTriggerEnter(Collider other)
    {
        if (collected) return; // Ignore if already collected

        if (other.CompareTag("Player"))
        {
            // Get reference to the PlayerGen script
            PlayerGen player = other.GetComponent<PlayerGen>();
            if (player != null)
            {
                // Add to inventory and handle collection
                player.AddCollectable(value);

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
                if (audioSource != null && collectSound != null)
                {
                        audioSource.PlayOneShot(collectSound);
                        Debug.Log("Playing collect sound");
                }
            }
            else
            {
                Debug.LogWarning("No PlayerGen script found on the player!");
            }
        }
    }

/*
    public IEnumerator Collect()
    {
        collected = true;

        // Play animation or effects here
        // PlayCollectAnimation();
        yield return new WaitForSeconds(destroyDelay); // Adjust this duration to match animation time

        // Destroy the object after the animation
        Destroy(gameObject);
    }
    
     private void PlayCollectAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Collected"); // Trigger the animation
        }
    }
     */
}
