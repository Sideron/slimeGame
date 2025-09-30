using UnityEngine;

public class StickySlime : Slime
{
    private GameObject stickedObject;
    Rigidbody2D myrb;
    bool wallSticked = false;
    void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
    }
    public override void onTouch(Rigidbody2D rb)
    {
        if (wallSticked && stickedObject==null) {
            rb.bodyType = RigidbodyType2D.Static;
            stickedObject = rb.gameObject;
        }
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        Rigidbody2D colliderRb = collider.transform.GetComponent<Rigidbody2D>();
        if (colliderRb == null)
        {
            myrb.bodyType = RigidbodyType2D.Static;
            wallSticked = true;
        }
    }
}
