using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [Header("Movement")]
     public float moveSpeed;
     public float groundDrag;
     public float maxRotationAngle = 135f;
     public float minRotationAngle = 45f;

     [Header("Ground Check")]
     public float playerHeight;
     public LayerMask whatIsGround;
     private bool grounded;

     [Header("Slope Handling")]
     public float maxSlopeAngle;
     private RaycastHit slopeHit;

     [Header("Sliding")]
     public float slideForce = 10f;
     public float maxSpeed = 15f;
     public float slopeThreshold = 5f;
     public float sideSlideForce = 10f;
     public float downhillBrakeForce = 5f;

     private Rigidbody rb;
     private float horizontalInput;
     private bool canMove = true;
     private bool sliding;

     [Header("Cutscene")]
     public bool cutsceneEnded = false;

     [Header("References")]
     public Transform orientation;
     public Transform playerObj;

     private float currentRotation = 90f;

     private void Start()
     {
          rb = GetComponent<Rigidbody>();
          rb.freezeRotation = true;
     }

     private void Update()
     {
          if (cutsceneEnded)
          {
               rb.constraints = RigidbodyConstraints.None;
               rb.freezeRotation = true;
          }
          else
          {
               rb.constraints = RigidbodyConstraints.FreezeAll;
          }
          // Ground check
          grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
          rb.drag = grounded ? groundDrag : 0;

          // Input handling
          horizontalInput = Input.GetAxisRaw("Horizontal");

          // Sliding logic
          if (OnSlope() && GetSlopeAngle() > slopeThreshold)
          {
               canMove = false;
               if (!sliding) StartSlide();
          }
          else
          {
               canMove = true;
               sliding = false;
          }

          float targetRotation = currentRotation + (horizontalInput * moveSpeed * Time.deltaTime * 10);

          // Clamp the rotation to the specified range
          targetRotation = Mathf.Clamp(targetRotation, minRotationAngle, maxRotationAngle);

          // Apply rotation to `orby` (child object)
          playerObj.localRotation = Quaternion.Euler(0, targetRotation, 37);

          // Update the current rotation for clamping continuity
          currentRotation = targetRotation;

     }

     private void FixedUpdate()
     {
          if (sliding)
          {
               Sliding();
               LimitSpeed();
          }
          else if (canMove)
          {
               FreeMovement();
               LimitSpeed();
          }

     }

     private void StartSlide()
     {
          sliding = true;
          rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
     }

     private void Sliding()
     {
          Vector3 downwardForce = OnSlope() ? GetSlopeMoveDirection(Vector3.down) : Vector3.down;
          rb.AddForce(downwardForce * slideForce, ForceMode.Force);

          Vector3 sideMovement = orientation.right * horizontalInput;
          rb.AddForce(sideMovement * slideForce * 0.5f, ForceMode.Force);

          if (rb.velocity.magnitude > maxSpeed)
          {
               rb.AddForce(-rb.velocity.normalized * downhillBrakeForce, ForceMode.Force);
          }
     }

     private void LimitSpeed()
     {
          if (rb.velocity.magnitude > maxSpeed)
          {
               rb.velocity = rb.velocity.normalized * maxSpeed;
          }
     }

     private void FreeMovement()
     {
          Vector3 sideMovement = orientation.right * horizontalInput * sideSlideForce;
          rb.AddForce(sideMovement * moveSpeed, ForceMode.Force);
     }

     private float GetSlopeAngle()
     {
          if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f, whatIsGround))
          {
               return Vector3.Angle(Vector3.up, slopeHit.normal);
          }
          return 0f;
     }

     public bool OnSlope()
     {
          // Perform a raycast to check if the player is on a slope
          if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f, whatIsGround))
          {
               // Get the angle between the slope normal and Vector3.up (vertical)
               float angle = Vector3.Angle(Vector3.up, slopeHit.normal);


               // Ensure the slope angle is within acceptable range
               return slopeHit.collider != null && angle > 0 && angle <= maxSlopeAngle;
          }

          // If raycast doesn't hit anything or player is not on a slope
          return false;
     }

     private Vector3 GetSlopeMoveDirection(Vector3 direction)
     {
          return slopeHit.collider != null ? Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized : Vector3.zero;
     }

     public Vector3 GetSlopeNormal()
     {
          if (OnSlope())
          {
               return slopeHit.normal; // slopeHit is the RaycastHit from OnSlope()
          }
          return Vector3.up; // Default normal (flat surface)
     }

}

