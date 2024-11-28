using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowboardAlignment : MonoBehaviour
{
    public Transform snowboard;
    public Transform player;
    public float alignmentSpeed = 5f;
    public Vector3 snowboardOffset; // Offset to keep snowboard under the player

    private PlayerMovement playerMovement;

    // Snowboard alignment offset (adjust these values to fit your model's orientation)
    public Vector3 snowboardAlignmentOffset = new Vector3(90f, 0f, 0f); // Example: Rotate by 180 on the Z-axis to match your needs

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        snowboardOffset = snowboard.position - player.position; // Set initial offset from player
    }

    // Update is called once per frame
    void Update()
    {
        AlignSnowboardToSlope();
    }

    private void AlignSnowboardToSlope()
    {
        if (playerMovement.OnSlope())
        {
            // Get the normal of the slope
            Vector3 slopeNormal = playerMovement.GetSlopeNormal();

            // Position the snowboard slightly behind the player, relative to the slope
            Vector3 targetPosition = player.position + snowboardOffset;

            // Adjust position to stay close to the player, but on the slope
            snowboard.position = Vector3.Lerp(snowboard.position, targetPosition, Time.deltaTime * 10f);

            // Calculate the forward direction of the snowboard (relative to the slope's normal)
            // To make the tail point backwards, we need to rotate the snowboard 180 degrees from its default forward direction.
            Quaternion targetRotation = Quaternion.LookRotation(player.forward, slopeNormal);

            // Apply an additional offset to rotate the snowboard's alignment
            targetRotation *= Quaternion.Euler(snowboardAlignmentOffset); // Apply offset for the snowboard's orientation

            // Smoothly rotate the snowboard to the target rotation
            snowboard.rotation = Quaternion.Slerp(snowboard.rotation, targetRotation, Time.deltaTime * alignmentSpeed);
        }
        else
        {
            // When not on a slope, the snowboard stays under the player with the same orientation
            snowboard.position = player.position + snowboardOffset;
            //snowboard.rotation = player.rotation; // Keep snowboard rotation aligned with player rotation (flat)
        }
    }
}
