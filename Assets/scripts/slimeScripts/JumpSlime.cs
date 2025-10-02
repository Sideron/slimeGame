using UnityEngine;

public class JumpSlime : Slime
{
    [SerializeField]
    private float impulseForce = 10;
    public override void onTouch(Rigidbody2D rb)
    {
        if (rb.linearVelocity.y != 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce((-transform.position + rb.transform.position).normalized * impulseForce * 10, ForceMode2D.Impulse);
        }
    }
    public override void onRelease(Rigidbody2D rb){}
}
