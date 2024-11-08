using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMovement : MonoBehaviour
{
     [Header("References")]
     public Transform orientation;
     public Transform playerObj;
     private Rigidbody rb;
     private PlayerMovement pm;

     [Header("Sliding")]
     public float maxSlideTime;
     public float slideForce;
     private float slideTimer;

     public float slideYScale;
     private float startYScale;

     private float horizontalInput;
     private float verticalInput;

     private bool sliding;

     private void Start()
     {
          rb = GetComponent<Rigidbody>();
          pm = GetComponent<PlayerMovement>();

          startYScale = playerObj.localScale.y;
     }

     private void Update()
     {
          horizontalInput = Input.GetAxisRaw("Horizontal");
          verticalInput = Input.GetAxisRaw("Vertical");

          if (Input.GetKeyDown(KeyCode.LeftControl) && (horizontalInput != 0 || verticalInput !=0))
          {
               StartSlide();
          }

          if (Input.GetKeyUp(KeyCode.LeftControl) && sliding)
          {
               StopSlide();
          }
     }

     private void FixedUpdate()
     {
          if (sliding)
          {
               Sliding();
          }
     }

     private void StartSlide()
     {
          sliding = true;

          playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
          rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

          slideTimer = maxSlideTime;
     }

     private void Sliding()
     {
          Vector3 inputdirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

          // Sliding normal
          if (!pm.OnSlope() || rb.velocity.y > -0.1f)
          {
               rb.AddForce(inputdirection.normalized * slideForce, ForceMode.Force);

               slideTimer -= Time.deltaTime;
          }

          // Sliding down a slope
          else
          {
               rb.AddForce(pm.GetSlopeMoveDirection(inputdirection) * slideForce, ForceMode.Force);
          }

          if (slideTimer <= 0)
          {
               StopSlide();
          }
     }

     private void StopSlide()
     {
          sliding = false;

          playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
     }
}
