using Unity.VisualScripting;
using UnityEngine;

public class JumpSlime : Slime
{
    [SerializeField]
    private float impulseForce = 10;
    [SerializeField] private AudioClip[] jump;
    private Animator myAnim;
    void Start()
    {
        transform.position = transform.position + new Vector3(0, -0.36f, 0);
        myAnim = GetComponent<Animator>();
        Invoke("animUnabled", 0.3f);
        Debug.Log(myAnim.name);
    }
    public override void onTouch(Rigidbody2D rb)
    {
        base.onTouch(rb);
        if (rb.linearVelocity.y > 0.01f || rb.linearVelocity.y < -0.01f)
        {
            pushObject(rb);
        }
    }

    public override void onShoot(Rigidbody2D rb)
    {
        base.onShoot(rb);
        bool isGrounded = rb.GetComponent<playerController>().isGrounded;
        if (!isGrounded)
        {
            pushObject(rb);
        }
    }

    private void pushObject(Rigidbody2D rb)
    {
        AudioManager.Instance.PlayRandomSFX(jump);
        ;
        if (myAnim)
        {
            myAnim.enabled = true;
            Invoke("animUnabled", 0.3f);
        }
        float acutalSpeed = rb.linearVelocity.magnitude;
        rb.linearVelocity = (-transform.position + rb.transform.position).normalized * (acutalSpeed >= impulseForce ? acutalSpeed : impulseForce);
    }
    void animUnabled()
    {
        myAnim.Rebind();
        myAnim.enabled = false;
    }
}
