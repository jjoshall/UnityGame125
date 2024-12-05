using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public AudioSource backgroundSnowboard;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        backgroundSnowboard = GameObject.Find("BackgroundSnowboard").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.cutsceneEnded && Time.timeScale != 0)
        {
            if (!backgroundSnowboard.isPlaying)
            {
                backgroundSnowboard.Play();
            }
        }
        else
        {
            if (Time.timeScale == 0)
            {
                if (backgroundSnowboard.isPlaying)
                {
                    backgroundSnowboard.Pause();
                }
            }
        }
    }

    public void PlaySound(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
