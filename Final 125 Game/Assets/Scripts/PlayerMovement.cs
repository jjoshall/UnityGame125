using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [Header("Movement")]
     private float moveSpeed;
     public float groundDrag;

     [Header("Ground Check")]
     public float playerHeight;
     public LayerMask whatIsGround;
     private bool grounded;

     [Header("Slope Handling")]
     public float maxSlopeAngle;
     private RaycastHit slopeHit;

     public Transform orientation;

     private float horizontalInput;

     private Rigidbody rb;

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
          if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f, whatIsGround))
          {
               return Vector3.Angle(Vector3.up, slopeHit.normal);
          }
          return 0f;
     }

     public bool OnSlope()
     {
          if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f, whatIsGround))
          {
               float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
               return slopeHit.collider != null && angle > 0 && angle <= maxSlopeAngle;
          }
          return false;
     }

     public Vector3 GetSlopeMoveDirection(Vector3 direction)
     {
          return slopeHit.collider != null ? Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized : Vector3.zero;
     }
}
