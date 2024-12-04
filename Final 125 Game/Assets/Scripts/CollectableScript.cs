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
                StartCoroutine(Collect());
            }
            else
            {
                Debug.LogWarning("No PlayerGen script found on the player!");
            }
        }
    }

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
}
