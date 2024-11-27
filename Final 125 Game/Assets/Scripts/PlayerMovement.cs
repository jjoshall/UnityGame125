using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [Header("Movement")]
     public float moveSpeed;
     public float groundDrag;
     public float maxRotationAngle = 90f;

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

     [Header("References")]
     public Transform orientation;
     public Transform playerObj;

     private void Start()
     {
          rb = GetComponent<Rigidbody>();
          rb.freezeRotation = true;
     }

     private void Update()
     {
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

     private bool OnSlope()
     {
          if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f, whatIsGround))
          {
               float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
               return slopeHit.collider != null && angle > 0 && angle <= maxSlopeAngle;
          }
          return false;
     }

     private Vector3 GetSlopeMoveDirection(Vector3 direction)
     {
          return slopeHit.collider != null ? Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized : Vector3.zero;
     }

}

