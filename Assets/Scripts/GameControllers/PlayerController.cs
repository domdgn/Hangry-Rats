using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    private Transform spawnPoint;

    [Header("Ground Detection Settings")]
    public Transform groundCheck; // Assign an empty GameObject positioned slightly below the player
    public float groundCheckRadius = 0.2f; // Adjust the radius as needed
    public LayerMask groundLayer; // Assign the ground layer in the Inspector

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private bool isFacingRight = true;
    private bool isMoving = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        Respawn();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool("IsMoving", isMoving);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Respawn();
        }

        FlipHandling();
    }

    private void FlipHandling()
    {
        if (horizontalInput > 0)
        {
            gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        else if (horizontalInput < 0)
        {
            gameObject.transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
        }
    }

    public void Respawn()
    {
        GameObject spawnObj = GameObject.FindWithTag("SpawnPoint");
        if (spawnObj != null)
        {
            spawnPoint = spawnObj.transform;
            transform.position = spawnPoint.position;
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        AdjustGravity();
    }

    private void Move()
    {
        Vector2 targetVelocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = targetVelocity;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void AdjustGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            PickUpController.Instance.CollectPickup();
        }

        //else if (other.CompareTag("RespawnTrigger"))
        //{
        //    Debug.Log("respawn trigger hit");
        //    GameObject spawnObj = GameObject.FindWithTag("SpawnPoint");
        //    if (spawnObj != null)
        //    {
        //        spawnPoint = spawnObj.transform;
        //        transform.position = spawnPoint.position;
        //    }
        //    else
        //    {
        //        Debug.LogWarning("No spawn point found!");
        //        transform.position = Vector2.zero;
        //    }
        //}
    }
}
