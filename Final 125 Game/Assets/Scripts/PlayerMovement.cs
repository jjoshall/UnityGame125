using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [Header("Movement")]
     private float moveSpeed;
     public float groundDrag;

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

     Rigidbody rb;

     private void Start()
     {
          rb = GetComponent<Rigidbody>();
          rb.freezeRotation = true;
     }

     private void Update()
     {
          grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
          MyInput();

          rb.drag = grounded ? groundDrag : 0;
     }

     private void MyInput()
     {
          horizontalInput = Input.GetAxisRaw("Horizontal");
     }
     public float GetSlopeAngle()
     {
          return slopeHit.collider != null ? Vector3.Angle(Vector3.up, slopeHit.normal) : 0f;
     }

     // Slope detection methods, if needed by SlidingMovement.cs
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