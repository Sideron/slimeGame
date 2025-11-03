using UnityEngine;

public class StickySlime : Slime
{
    [SerializeField] private GameObject stickedObject;
    Rigidbody2D myrb;
    bool wallSticked = false;
    [SerializeField] private float viscosity = 50f;
    void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
    }
    public override void onTouch(Rigidbody2D rb)
    {
        if (wallSticked && stickedObject == null)
        {
            Debug.Log("Entered");
            rb.bodyType = RigidbodyType2D.Static;
            stickedObject = rb.gameObject;
            playerController player = rb.transform.GetComponent<playerController>();
            if (player != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.linearDamping = viscosity;
            }
        }
    }
    public override void onRelease(Rigidbody2D rb)
    {
        playerController player = rb.transform.GetComponent<playerController>();
        if (rb.gameObject == stickedObject && player)
        {
            Debug.Log("Released");
            stickedObject = null;
            if (player != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.linearDamping = 0;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        myrb.bodyType = RigidbodyType2D.Static;
        wallSticked = true;
    }
    public void setFree()
    {
        stickedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
