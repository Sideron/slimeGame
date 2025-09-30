using UnityEngine;

public class StickySlime : Slime
{
    private GameObject stickedObject;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public override void onTouch(Rigidbody2D rb)
    {

    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        Rigidbody2D colliderRb = collider.transform.GetComponent<Rigidbody2D>();
        if (colliderRb == null)
        {
        
        }
    }
}
