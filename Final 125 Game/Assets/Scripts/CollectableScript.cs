using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableScript : MonoBehaviour
{
    [Header("Settings")]
    public int value;

    [Header("References")]
    public Animator animator;
    public float destroyDelay = 0.5f;

    private bool collected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with collectable!");
        }
    }

    public void Collect()
    {
        if (collected) return; // Prevent multiple collections
        collected = true;

        PlayCollectAnimation();
    }
    private void PlayCollectAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Collected"); // Trigger the animation
        }

        // Destroy the object after the animation finishes
        Destroy(gameObject, destroyDelay);
        Debug.Log("Collected " + value + "!");
    }
}
