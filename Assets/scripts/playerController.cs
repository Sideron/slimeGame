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
    private bool isGrounded;
    private float moveInput;

    public StickySlime slimeTrap;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Comprobar si est� en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        // Movimiento horizontal
        moveInput = Input.GetAxisRaw("Horizontal");
        //if(isGrounded) { rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); }
        if (moveInput != 0)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x,moveInput * moveSpeed, 5f*Time.deltaTime), rb.linearVelocity.y);
            }
        }
            else if (isGrounded)
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }

        

        // Salto
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            if (slimeTrap != null)
            {
                Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                slimeTrap.setFree();
                //rb.linearVelocity = new Vector2((-slimeTrap.transform.position+transform.position).normalized.x*jumpForce/1.5f , jumpForce/1.5f);
                rb.linearVelocity = new Vector2((mouseWorld-transform.position).normalized.x*jumpForce , jumpForce);
                slimeTrap = null;
                
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualiza el groundCheck en la escena
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
