using UnityEngine;

public class RatController : MonoBehaviour
{
    [Header("Patrolling References")]
    [SerializeField] GameObject PointA;
    [SerializeField] GameObject PointB;
    [SerializeField] float speed;
    Rigidbody2D rb;
    Transform currentPoint;

    [Header("Tracking References")]
    [SerializeField] GameObject player;
    [SerializeField] float trackingRadius;
    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] Transform groundCheckObject;
    [SerializeField] LayerMask groundLayer;
    bool isGrounded;
    float directionX;

    [Header("Movement References")]
    [SerializeField] float jumpForce;
    bool isFacingRight = true;

    public enum RatStates
    {
        Patrolling,
        Tracking,
        Idle,
        Busy
    }

    [SerializeField] RatStates ratState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ratState = RatStates.Patrolling;
        currentPoint = PointB.transform;

        if (PointA != null) PointA.transform.parent = null;
        if (PointB != null) PointB.transform.parent = null;
    }

    void Update()
    {
        CheckGrounded();
        CheckPlayerDistance();

        switch (ratState)
        {
            case RatStates.Patrolling:
                Patrol();
                break;
            case RatStates.Tracking:
                TrackPlayer();
                break;
        }
    }

    void CheckPlayerDistance()
    {
        if (player != null && Vector2.Distance(transform.position, player.transform.position) < trackingRadius)
        {
            ratState = RatStates.Tracking;
        }
        else
        {
            ratState = RatStates.Patrolling;
        }
    }

    void Patrol()
    {
        if (PointA == null || PointB == null) return;

        if (currentPoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            Flip();
            currentPoint = (currentPoint == PointB.transform) ? PointA.transform : PointB.transform;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void CheckGrounded()
    {
        if (groundCheckObject != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckObject.position, groundCheckDistance, groundLayer);
        }
    }

    void TrackPlayer()
    {
        if (player == null) return;

        directionX = player.transform.position.x - transform.position.x;

        // Add a small deadzone to prevent jittering when X coordinates are very close
        if (Mathf.Abs(directionX) < 0.1f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        directionX = Mathf.Sign(directionX);
        rb.velocity = new Vector2(directionX * speed, rb.velocity.y);

        if (directionX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (directionX < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;

            // Calculate push direction based on relative positions
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;

            // Add upward force to prevent being pushed into the ground
            float pushForce = 10f;
            float upwardForce = 5f;

            // Ensure the rat is always pushed upward and horizontally, never downward
            pushDirection.y = Mathf.Abs(pushDirection.y);

            rb.AddForce(new Vector2(pushDirection.x * pushForce, upwardForce), ForceMode2D.Impulse);
            ratState = RatStates.Idle;
            Invoke("ResetState", 1f);
        }
    }

    void Jump()
    {
        if (!isGrounded) return;

        RaycastHit2D headCheck = Physics2D.Raycast(
            transform.position,
            Vector2.up,
            1f,
            groundLayer
        );

        if (!headCheck.collider && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ResetState()
    {
        ratState = RatStates.Patrolling;
    }
}