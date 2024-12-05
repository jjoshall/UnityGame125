using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameHitboxScript : MonoBehaviour
{
     public WinScript winScript;

     // If the player enters the hitbox, say they beat the level and restart the game.  
     private void OnTriggerEnter(Collider other)
     {
          if (other.gameObject.tag == "Player")
          {
               // unlock the cursor
               Cursor.lockState = CursorLockMode.None;
               Cursor.visible = true;

               StartCoroutine(ShowRestartMenuAfterPause(.1f));
          }
     }

     private IEnumerator ShowRestartMenuAfterPause(float delay)
     {
          yield return new WaitForSeconds(delay);

          // load the level completed ui
          winScript.ShowWinScreen();
     }
}
