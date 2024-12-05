using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCollectSound : MonoBehaviour
{
     // Reference the whole collectible object
     public GameObject collectable;

     // Making audio sounds
     public AudioClip collectSound;
     private AudioSource audioSource;

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
          if (other.gameObject.layer == LayerMask.NameToLayer("Crystal"))
          {
               // Play the collect sound
               if (audioSource != null && collectSound != null)
               {
                    audioSource.PlayOneShot(collectSound);
                    Debug.Log("Playing collect sound");
               }
          }
     }
}
