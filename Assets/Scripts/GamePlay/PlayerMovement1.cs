using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player
    public float jumpForce = 10f; // Force applied for jumping
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector2 moveInput; // Store movement input
    private bool isGrounded; // Check if the player is grounded

    public Transform groundCheck; // Transform to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius of the ground check
    public LayerMask groundLayer; // Layer that defines what is considered ground

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Get the input from the player
        float moveX = Input.GetAxis("Horizontal");
        moveInput = new Vector2(moveX, rb.velocity.y); // Horizontal movement

        // Check if the player presses the jump key and is grounded
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Apply the movement to the Rigidbody2D
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is colliding with the ground
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player has left the ground
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
}
