using Unity.VisualScripting;
using UnityEngine;

public class JumpSlime : Slime
{
    [SerializeField]
    private float impulseForce = 10;
    [SerializeField] private AudioClip[] jump;
    public override void onTouch(Rigidbody2D rb)
    {
        base.onShoot(rb);
        if (rb.linearVelocity.y > 0.01f || rb.linearVelocity.y < -0.01f)
        {
            pushObject(rb);
        }
    }

    public override void onShoot(Rigidbody2D rb)
    {
        base.onShoot(rb);
        if (rb.linearVelocity.y > 0.01f || rb.linearVelocity.y < -0.01f)
        {
            pushObject(rb);
        }
    }
    
    private void pushObject(Rigidbody2D rb)
    {
        AudioManager.Instance.PlayRandomSFX(jump);
        float acutalSpeed = rb.linearVelocity.magnitude;
        rb.linearVelocity = (-transform.position + rb.transform.position).normalized * (acutalSpeed >= impulseForce ? acutalSpeed : impulseForce);
    }
}
