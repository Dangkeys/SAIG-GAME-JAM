using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask macroBlockLayerMask;

    [Header("Player 2 Settings")]
    [SerializeField] private float player2JumpForceMultiplier = 0.3f;

    [Header("Scaling Settings")]
    [SerializeField] private float minimumScale = 0.5f;
    [SerializeField] private float scaleUpFactor = 3f;
    [SerializeField] private float massScaleMultiplier = 1f;

    [Header("References")]
    private Rigidbody2D rb;
    private Vector2 movementInput;
    [field: SerializeField] public SpriteRenderer PlayerSprite { get; private set; }
    [field: SerializeField] public Animator PlayerAnimator { get; private set; }

    [Header("State")]
    private bool isOnGround;
    private Vector3 initialScale;
    private bool isScaled = false;
    [field: SerializeField] public bool isPlayerOne { get; private set; }
    private AudioManager audioManager;
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        ProcessMovement();
        ProcessJump();
        ProcessScaling();
    }

    private void FixedUpdate()
    {
        CheckIfOnGround();
        ApplyMovement();
    }

    private void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
        audioManager = AudioManager.Instance;
    }

    private void ProcessMovement()
    {
        float moveX = isPlayerOne ? Input.GetAxis("Horizontal") : Input.GetAxis("Player2Horizontal");

        movementInput = new Vector2(moveX, rb.velocity.y);
        audioManager.WalkingSound(moveX != 0 && isOnGround, isPlayerOne);
        HandleSprite(moveX);
    }

        
    private void HandleSprite(float moveX)
    {
        PlayerAnimator.SetBool("IsWalk", Mathf.Abs(moveX) > 0.1f);
        if (moveX > 0)
        {
            PlayerSprite.flipX = false; // Face right
        }
        else if (moveX < 0)
        {
            PlayerSprite.flipX = true;  // Face left
        }
    }
    private void ProcessJump()
    {
        if (isOnGround && Input.GetKeyDown(GetJumpKey()))
        {
            ExecuteJump();
        }
    }

    private void ProcessScaling()
    {
        if (Input.GetKeyDown(GetScaleKey()))
        {
            ToggleScaling();
        }
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementInput.x * movementSpeed, rb.velocity.y);
    }

    private void ExecuteJump()
    {
        float jumpMultiplier = isPlayerOne || !isScaled ? 1 : player2JumpForceMultiplier;
        if (isPlayerOne && isScaled)
        {
            jumpMultiplier = 0;
        }
        rb.AddForce(Vector2.up * (jumpForce * jumpMultiplier), ForceMode2D.Impulse);
        if(jumpMultiplier > 0)
        {
            audioManager.PlaySound(0);
        }
    }

    private void ToggleScaling()
    {
        if (isScaled)
        {
            ResetScale();
        }
        else
        {
            Vector3 newScale = isPlayerOne ? initialScale * scaleUpFactor : new Vector3(minimumScale, minimumScale, 1f);
            transform.localScale = newScale;
            rb.mass = newScale.x * newScale.y * massScaleMultiplier;
            if(isPlayerOne)
            {
                audioManager.PlaySound(10);
            }
            else
            {
                audioManager.PlaySound(9);
            }
            isScaled = true;
        }
    }

    private void ResetScale()
    {
        transform.localScale = initialScale;
        rb.mass = initialScale.x * initialScale.y * massScaleMultiplier;
        audioManager.PlaySound(11);
        isScaled = false;
    }

    private KeyCode GetJumpKey()
    {
        return isPlayerOne ? KeyCode.W : KeyCode.UpArrow;
    }

    private KeyCode GetScaleKey()
    {
        return isPlayerOne ? KeyCode.S : KeyCode.DownArrow;
    }

    private void CheckIfOnGround()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundLayerMask);
    }

    public bool CheckIfOnMacroBlock()
    {
        return Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, macroBlockLayerMask);
    }
    public void Die()
    {
        SceneManager.Instance.ReloadCurrentScene();
    }
}
