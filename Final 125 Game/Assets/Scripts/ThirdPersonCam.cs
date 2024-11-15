using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used this video for Third-Person Camera: https://www.youtube.com/watch?v=UCwwn2q4Vys&ab_channel=Dave%2FGameDevelopment

/*
 * WE ARE NOT USING THIS SCRIPT AT THE MOMENT. THIS WAS FOR THE 
 * THIRD PERSON CAMERA IF WE WANT THE PLAYER TO BE ABLE TO LOOK AROUND.
 * THIS USES THE CINEMACHINE PACKAGE IN UNITY.
*/

public class ThirdPersonCam : MonoBehaviour
{
     [Header("References")]
     public Transform orientation;
     public Transform player;
     public Transform playerObj;
     public Rigidbody rb;

     public float rotationSpeed;

     private void Start()
     {
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
     }
     
     private void Update()
     {
          // rotate the orientation
          Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
          orientation.forward = viewDir.normalized;

          // rotate the player
          float horizontalInput = Input.GetAxis("Horizontal");
          float verticalInput = Input.GetAxis("Vertical");
          Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

          if (inputDir != Vector3.zero)
          {
               playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
          }
     }
}
