using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersJump : MonoBehaviour
{
    [SerializeField] float jumpForce = 10f; // The force applied to the player when jumping
    [SerializeField] float jumpDelay = 0.2f; // The delay before the player can jump again
    [SerializeField] float jumpGracePeriod = 0.1f;// The time period in which the player can still jump after leaving the ground
    [SerializeField] float maxJumpDistance = 2f; // The maximum distance the player can jump
    
    //[SerializeField] float jumpHeight = 5;
    //[SerializeField] float gravityScale = 5;
    //[SerializeField] float fallGravityScale = 15;

    private bool canJump = false; // A boolean that keeps track of whether the player can jump
    private bool isGrounded = false; // A boolean that keeps track of whether the player is grounded
    private Rigidbody2D rb; // The rigidbody component of the player
    private Transform currentNut; // The current nut that the player is on
    private RigidbodyType2D originalRigidbodyType;// The original rigidbody type of the player

    public object velocity { get; internal set; }

    void Start()
    {
        // Get the rigidbody component of the player
        rb = GetComponent<Rigidbody2D>();
        originalRigidbodyType = rb.bodyType;
    }

    void Update()
    {
        // Check if the player can jump and is grounded, and the mouse button is clicked
        if (Input.GetMouseButtonDown(0) && canJump && isGrounded)
        {
            // Call JumpToNut function to make the player jump
            JumpToNut();
        }
    }

    void JumpToNut()
    {
        // The player is not grounded anymore after jumping
        isGrounded = true;

        // The player can't jump anymore until they land on another nut
        canJump = true;

        // Reset the velocity of the player
        rb.velocity = Vector2.zero;

        // Calculate the direction to jump in
        Vector2 direction = (currentNut.position - transform.position).normalized;

        // Calculate the distance to jump
        float distance = Vector2.Distance(currentNut.position, transform.position);
        float jumpDistance = Mathf.Min(distance, maxJumpDistance);

        //rb.gravityScale = gravityScale;

        // Apply force to the rigidbody of the player to make them jump
        rb.AddForce(direction * jumpForce * jumpDistance, ForceMode2D.Impulse);
        //jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;

        // Enable jumping again after a delay
        Invoke("EnableJump", jumpDelay);
    }

    void EnableJump()
    {
        // The player can jump again
        canJump = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with a nut and the nut is not the current nut the player is on
        if (collision.gameObject.CompareTag("nut") && collision.transform != currentNut)
        {
            // Set the current nut to the nut the player collided with
            currentNut = collision.transform;

            // The player is now grounded on the nut
            isGrounded = true;

            // Make the player a child of the nut so that they move with it
            transform.SetParent(currentNut);

            // Make the nut kinematic so that it doesn't affect the player's movement
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the player is still colliding with the same nut, and is within range to jump to another nut
        if (collision.gameObject.CompareTag("nut"))
        {
            float distance = Vector2.Distance(collision.transform.position, transform.position);
            if (distance < maxJumpDistance + jumpGracePeriod)
            {
                // Set the current nut to the nut the player collided with
                currentNut = collision.transform;

                // The player is now grounded on the nut
                isGrounded = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player has exited the current nut
        if (collision.transform == currentNut)
        {
            // The player is no longer grounded
            isGrounded = false;

            // Detach the player from the nut
            transform.SetParent(null);

            // Restore the original rigidbody type of the player
            rb.bodyType = originalRigidbodyType;
        }
    }
}
