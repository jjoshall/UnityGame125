using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCardConntroller : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private WispNavigation wispNavigation;
    public AudioSource raceStart321;
    public AudioSource raceStartGo;
    // Start is called before the first frame update
    void Start()
    {
        // retrieve it as a component of the game object "Player"
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        // retrieve it as a component of the game object "Wisp"
        wispNavigation = GameObject.Find("AI Wisp").GetComponent<WispNavigation>();
        // retrieve as componnnennt of the game object "RaceStartSound"
        raceStart321 = GameObject.Find("RaceStart321").GetComponent<AudioSource>();
        raceStartGo = GameObject.Find("RaceStartGo").GetComponent<AudioSource>();
    }

    void UnlockPlayerMovement()
    {
        playerMovement.cutsceneEnded = true;
        wispNavigation.cutsceneEnded = true;
    }

    void PlayRaceStartSound()
    {
        if (raceStart321 != null)
        {
            raceStart321.Play();
        }
    }
    void PlayRaceStartGoSound()
    {
        if (raceStartGo != null)
        {
            raceStartGo.Play();
        }
    }
}
