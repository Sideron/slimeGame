using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Suelo")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    [SerializeField]
    private Animator anim;

    public bool isGrounded;
    private float moveInput;
    public float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    [SerializeField] private AudioClip[] jumpSound;
    [SerializeField] private AudioClip pisadas;
    [SerializeField] private float stepInterval = 0.1f;
    private float stepTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        coyoteTimeCounter = isGrounded ? coyoteTime : coyoteTimeCounter - Time.deltaTime;
        moveInput = Input.GetAxisRaw("Horizontal");

        anim.enabled = (isGrounded && Mathf.Abs(moveInput) > 0);
        if(!anim.enabled)
        {
            anim.Rebind();
        }

        if (moveInput != 0)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

                stepTimer -= Time.deltaTime;
                if (stepTimer <= 0f)
                {
                    AudioManager.Instance.PlaySFX(pisadas);
                    stepTimer = stepInterval;
                }
            }
            else
            {
                rb.linearVelocity = new Vector2(
                    Mathf.Lerp(rb.linearVelocity.x, moveInput * moveSpeed, 5f * Time.deltaTime),
                    rb.linearVelocity.y
                );
            }
        }
        else if (isGrounded)
        {
            stepTimer = 0f;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0)
        {
            AudioManager.Instance.PlayRandomSFX(jumpSound);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            coyoteTimeCounter = 0;
        }
    }

    public void Dash()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.linearVelocity = new Vector2((mouseWorld - transform.position).normalized.x * jumpForce, jumpForce);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
