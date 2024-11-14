using UnityEngine;

public class SlidingMovement : MonoBehaviour
{
     [Header("References")]
     public Transform orientation;
     public Transform playerObj;
     private Rigidbody rb;
     private PlayerMovement pm;

     [Header("Sliding")]
     public float slideForce;
     public float maxSpeed = 15f;
     public float slopeThreshold = 5f; // Angle threshold to start sliding (adjust as needed)
     public float sideSlideForce = 10f;

     private float horizontalInput;

     private bool canMove = true; // Allows free movement on flat ground
     private bool sliding;

     private void Start()
     {
          rb = GetComponent<Rigidbody>();
          pm = GetComponent<PlayerMovement>();
          slideForce = 10f;
     }

     private void Update()
     {
          horizontalInput = Input.GetAxisRaw("Horizontal") * sideSlideForce;

          // Check if we're on a slope
          if (pm.OnSlope() && pm.GetSlopeAngle() > slopeThreshold)
          {
               canMove = false; // Disable free movement once on slope
               if (!sliding) StartSlide();
          }
          else
          {
               canMove = true; // Allow free movement on flat ground
          }
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
               // Allow free movement if still at the top
               Vector3 sideMovement = orientation.right * horizontalInput;
               rb.AddForce(sideMovement * slideForce, ForceMode.Force);
          }
     }

     private void StartSlide()
     {
          sliding = true;
          rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
     }

     private void Sliding()
     {
          Vector3 downwardForce = pm.OnSlope() ? pm.GetSlopeMoveDirection(Vector3.down) : Vector3.down;
          rb.AddForce(downwardForce * slideForce, ForceMode.Force);

          // Allow side movement while sliding, but limit its effect
          Vector3 sideMovement = orientation.right * horizontalInput;
          rb.AddForce(sideMovement * slideForce * 0.5f, ForceMode.Force);
     }

     private void LimitSpeed()
     {
          Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
          if (flatVelocity.magnitude > maxSpeed)
          {
               Vector3 clampedVelocity = flatVelocity.normalized * maxSpeed;
               rb.velocity = new Vector3(clampedVelocity.x, rb.velocity.y, clampedVelocity.z);
          }
     }
}