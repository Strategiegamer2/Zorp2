using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovment : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acceleration = 15f;
    public float deceleration = 15f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private int groundContactCount = 0;
    private bool isFacingRight = true;
    private float currentSpeed;
    private float lastDirection;
    private float previousSpeed;

    [SerializeField] private Transform startPoint; // The start point for respawn

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (startPoint == null)
        {
            startPoint = new GameObject("Start Point").transform;
            startPoint.position = transform.position;
        }
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        HandleMovement(moveX);

        // Jumping
        HandleJumping();

        // Check for respawn
        CheckForRespawn();
    }

    private void HandleMovement(float moveX)
    {
        bool isMoving = moveX != 0;
        bool hasDirectionChanged = (Mathf.Sign(moveX) != Mathf.Sign(lastDirection) && moveX != 0) || (isFacingRight && moveX < 0) || (!isFacingRight && moveX > 0);

        if (isMoving)
        {
            // Accelerate or maintain current speed
            if (hasDirectionChanged)
            {
                currentSpeed = 0; // Reset speed when changing direction
                FlipCharacter();
            }

            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
        else
        {
            // Decelerate
            currentSpeed -= deceleration * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);
        }

        lastDirection = moveX;
        rb.velocity = new Vector2(moveX * currentSpeed, rb.velocity.y);

        previousSpeed = currentSpeed;
    }

    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump") && groundContactCount > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // Modify gravity for more realistic jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void CheckForRespawn()
    {
        if (transform.position.y < -15)
        {
            // Respawn at the start point
            transform.position = startPoint.position;
            rb.velocity = Vector2.zero; // Reset velocity to prevent falling through the ground
        }
    }

    public void CheckForRespawnDeath()
    {
        // Respawn at the start point
        transform.position = startPoint.position;
        rb.velocity = Vector2.zero; // Reset velocity to prevent falling through the ground
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContactCount++;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContactCount--;
        }
    }
}
