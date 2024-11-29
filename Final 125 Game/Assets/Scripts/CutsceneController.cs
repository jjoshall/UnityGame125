using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public PlayerMovement playerController;  
    public WispNavigation wispNavigation;    
    public GameObject countdownPanel;         
    public PlayableDirector playableDirector; 
    public Rigidbody playerRigidbody;         

    // public Animation countdownAnimation;    
    public Animator countdownAnimator;

    void Start()
    {
        // Find the Animator on the countdown panel
        countdownAnimator = countdownPanel.GetComponent<Animator>();

        // Disable player movement
        playerController.cutsceneEnded = false;

        //Disable wisp movement
        wispNavigation.cutsceneEnded = false;

        // Freeze the player's Rigidbody
        playerRigidbody.isKinematic = true;

        // Subscribe to the PlayableDirector's stopped event
        playableDirector.stopped += OnCutsceneFinished;

        // disable the script playerController and wispNavigation
        playerController.enabled = false;
        wispNavigation.enabled = false;

    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        playableDirector.stopped -= OnCutsceneFinished;
    }

    void OnCutsceneFinished(PlayableDirector pd)
    {
        // Enable the countdown panel
        countdownPanel.SetActive(true);


        // Play the countdown animation
        if (countdownAnimator != null)
        {
            countdownAnimator.SetTrigger("PlayAnimation");
        }
        else
        {
            Debug.LogError("countdown not found on CountdownPanel!");
        }
    }

    public void OnCountdownAnimationComplete()
    {
        // Disable the countdown panel
        countdownPanel.SetActive(false);
        Debug.Log("countdown panel set inactive!");
        
        // Unfreeze the player's Rigidbody
        playerRigidbody.isKinematic = false;
        Debug.Log("player rigidbody can move!");
        
        // Enable player movement
        playerController.cutsceneEnded = true;
        Debug.Log("player movement enabled (cutsceneEnded)!");

        // Enable wisp movement
        wispNavigation.cutsceneEnded = true;
        Debug.Log("wisp movement enabled (cutsceneEnded)!");

        // Disable the CutsceneController script
        this.enabled = false;

        // enable the script playerController and wispNavigation
        playerController.enabled = true;
        wispNavigation.enabled = true;
    }
}
