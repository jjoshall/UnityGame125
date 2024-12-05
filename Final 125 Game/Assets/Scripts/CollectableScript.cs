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
    public float destroyDelay = 0.5f;
     
    // Reference the player's particle effects
    public ParticleSystem playerExplosion;

     // Reference the whole collectible object
     public GameObject collectable;     

     private bool collected = false;

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

                    // Start couroutine to destroy the object
                    StartCoroutine(DestroyCollectable());
               }
               else
            {
                Debug.LogWarning("No PlayerGen script found on the player!");
            }
        }
    }

     private IEnumerator DestroyCollectable()
     {
          collected = true;
          yield return new WaitForSeconds(destroyDelay);
          collectable.SetActive(false);
     }
}
