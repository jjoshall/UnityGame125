using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Used this video for player movement: 
https://www.youtube.com/watch?v=f473C43s8nE&t=380s&ab_channel=Dave%2FGameDevelopment
https://www.youtube.com/watch?v=xCxSjgYTw9c&ab_channel=Dave%2FGameDevelopment
*/

public class PlayerMovement : MonoBehaviour
{
     [Header("Movement")]
     private float moveSpeed;
     public float walkSpeed;
     public float sprintSpeed;

     public float groundDrag;

     [Header("Jumping")]
     public float jumpForce;
     public float jumpCooldown;
     public float airMultiplier;
     bool readyToJump;

     [Header("Ground Check")]
     public float playerHeight;
     public LayerMask whatIsGround;
     bool grounded;

     [Header("Slope Handling")]
     public float maxSlopeAngle;
     private RaycastHit slopeHit;
     private bool exitingSlope;

     public Transform orientation;

     float horizontalInput;
     float verticalInput;

     Vector3 moveDirection;

     Rigidbody rb;

     public MovementState state;
     public enum MovementState
     {
          walking,
          sprinting,
          air
     }

     private void Start()
     {
          rb = GetComponent<Rigidbody>();
          rb.freezeRotation = true;

          readyToJump = true;
     }

     private void Update()
     {
          // ground check
          grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

          MyInput();
          SpeedControl();
          StateHandler();

          if (grounded)
          {
               rb.drag = groundDrag;
          }
          else
          {
               rb.drag = 0;
          }
     }

     private void FixedUpdate()
     {
          MovePlayer();
     }

     private void MyInput()
     {
          horizontalInput = Input.GetAxisRaw("Horizontal");
          verticalInput = Input.GetAxisRaw("Vertical");

          // when to jump
          if (Input.GetButtonDown("Jump") && grounded && readyToJump)
          {
               readyToJump = false;

               Jump();

               Invoke(nameof(ResetJump), jumpCooldown);
          }
     }

     private void StateHandler()
     {
          // Mode - Sprinting
          if (Input.GetKey(KeyCode.LeftShift) && grounded)
          {
               state = MovementState.sprinting;
               moveSpeed = sprintSpeed;
          }
          // Mode - Walking
          else if (grounded)
          {
               state = MovementState.walking;
               moveSpeed = walkSpeed;
          }
          // Mode - Air
          else
          {
               state = MovementState.air;
          }
     }

     private void MovePlayer()
     {
          moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

          // On slope
          if (OnSlope() && !exitingSlope)
          {
               rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

               if (rb.velocity.y > 0)
               {
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);
               }
          }

          // On ground
          if (grounded)
          {
               rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
          }

          // In air
          else if (!grounded)
          {
               rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
          }

          // Turn gravity off when on slope
          rb.useGravity = !OnSlope();
     }

     private void SpeedControl()
     {
          // Limiting speed on slope
          if (OnSlope() && !exitingSlope)
          {
               if (rb.velocity.magnitude > moveSpeed)
               {
                    rb.velocity = rb.velocity.normalized * moveSpeed;
               }
          }

          // Limiting speed on flat ground/air
          else
          {
               Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

               // Limit the speed of the player
               if (flatVel.magnitude > moveSpeed)
               {
                    Vector3 limitedVel = flatVel.normalized * moveSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
               }
          }
     }

     private void Jump()
     {
          exitingSlope = true;
          
          // Reset y velocity
          rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

          rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
     }

     private void ResetJump()
     {
          readyToJump = true;

          exitingSlope = false;
     }

     public bool OnSlope()
     {
          if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
          {
               float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
               return angle < maxSlopeAngle && angle != 0;
          }

          return false;
     }

     public Vector3 GetSlopeMoveDirection(Vector3 direction)
     {
          return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
     }
}
