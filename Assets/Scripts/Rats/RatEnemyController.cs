using UnityEngine;

public class RatEnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public float jumpForce = 5f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Transform player;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isFacingRight = true;

  /* 
   TODO: 
    - Add tolerance so the rat doesnt freak out when x = same as player
    - Prevent infinite hits in 1 second
    - STATE SYSTEM so rats can be docile when player not carrying food
    - Make platforming of rat work better (perhaps with A* pathfinding)
    - Rat & button interactions
    - Rat only jumps when within radius of player perhaps - prevents all rats in scene jumping whenever the player does
  */

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CheckGround();
        MoveTowardsPlayer();

        // Jump if player is above rat
        if (isGrounded && player.position.y > transform.position.y + 3f)
        {
            Jump();
        }
    }

    void MoveTowardsPlayer()
    {
        // Get direction to player (ignoring y-axis)
        float moveDirection = player.position.x > transform.position.x ? 1 : -1;

        // Move in that direction
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

        if (moveDirection > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveDirection < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void CheckGround()
    {
        // Creates a circle at groundCheck position to check if touching ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Flip()
    {
        // Flip the sprite by scaling
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}