using System;
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

    public bool CanUseSkill = true;

    [Header("State")]
    private bool isOnGround;
    private Vector3 initialScale;
    [field: SerializeField] public bool IsScaled {get; private set;} = false;
    [field: SerializeField] public bool IsPlayerOne { get; private set; }
    private bool hasJumped;
    private AudioManager audioManager;

    public event Action<bool> OnChangeScale;
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        hasJumped = !isOnGround;

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
        float moveX = IsPlayerOne ? Input.GetAxis("Horizontal") : Input.GetAxis("Player2Horizontal");

        movementInput = new Vector2(moveX, rb.linearVelocity.y);
        audioManager.WalkingSound(moveX != 0 && isOnGround, IsPlayerOne);
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

        if (isOnGround && Input.GetKey(GetJumpKey()) && !hasJumped)
        {
            ExecuteJump();
            hasJumped = true;
        }
    }


    private void ProcessScaling()
    {
        if (Input.GetKeyDown(GetScaleKey()) && Time.timeScale > 0)
        {
            TryToggleScaling();
        }
    }

    private void ApplyMovement()
    {
        rb.linearVelocity = new Vector2(movementInput.x * movementSpeed, rb.linearVelocity.y);
    }

    private void ExecuteJump()
    {

        float jumpMultiplier = IsPlayerOne || !IsScaled ? 1 : player2JumpForceMultiplier;
        if (IsPlayerOne && IsScaled)
        {
            jumpMultiplier = 0;
        }
        if (rb.linearVelocity.y <= 0.5)
        {
            rb.AddForce(Vector2.up * (jumpForce * jumpMultiplier), ForceMode2D.Impulse);
            if (jumpMultiplier > 0)
            {
                audioManager.PlaySound(0);
            }
        }

    }

    private void TryToggleScaling()
    {
        if (IsScaled)
        {
            if(!IsPlayerOne && !CanUseSkill) return;
            ResetScale();
        }
        else
        {
            if(IsPlayerOne && !CanUseSkill) return;
            Vector3 newScale = IsPlayerOne ? initialScale * scaleUpFactor : new Vector3(minimumScale, minimumScale, 1f);
            transform.localScale = newScale;
            rb.mass = newScale.x * newScale.y * massScaleMultiplier;
            if (IsPlayerOne)
            {
                audioManager.PlaySound(10);
            }
            else
            {
                audioManager.PlaySound(9);
            }
            IsScaled = true;
        }
        OnChangeScale?.Invoke(true);
    }

    private void ResetScale()
    {
        transform.localScale = initialScale;
        rb.mass = initialScale.x * initialScale.y * massScaleMultiplier;
        audioManager.PlaySound(11);
        IsScaled = false;
    }

    private KeyCode GetJumpKey()
    {
        return IsPlayerOne ? KeyCode.W : KeyCode.UpArrow;
    }

    private KeyCode GetScaleKey()
    {
        return IsPlayerOne ? KeyCode.S : KeyCode.DownArrow;
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

    public int MyWeight()
    {
        if(IsScaled)
        {
            if(IsPlayerOne)
            {
                return 2;
            }
            return 0;
        }
        return 1;
    }
}
