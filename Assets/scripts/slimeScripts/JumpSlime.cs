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
        // get angle of rb
        Vector2 rbPos = rb.transform.position;
        float angle = Mathf.Atan2(rbPos.y - transform.position.y, rbPos.x - transform.position.x) * Mathf.Rad2Deg;
        Debug.Log("Angle: "+angle);
        playerController isPlayer = rb.GetComponent<playerController>();
        if ((isPlayer && !isPlayer.isGrounded) || !isPlayer)
        {
            //pushObject(rb);
            
            if (angle > 45 && angle < 135)
            {
                // arriba
                pushObject(rb, new Vector2(0, 1));
                Debug.Log("arriba");
            }
            else if (angle < -45 && angle > -135)
            {
                // abajo
                pushObject(rb, new Vector2(0, -1));
                Debug.Log("abajo");
            }
            else if (angle >= -45 && angle <= 45)
            {
                // derecha
                pushObject(rb, new Vector2(1, 0.6f));
                Debug.Log("derecha");
            }
            else 
            {
                // izquierda
                pushObject(rb, new Vector2(-1, 0.6f));
                Debug.Log("izquierda");
            }
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
    private void pushObject(Rigidbody2D rb, Vector2 dir)
    {
        AudioManager.Instance.PlayRandomSFX(jump);
        ;
        if (myAnim)
        {
            myAnim.enabled = true;
            Invoke("animUnabled", 0.3f);
        }
        Vector2 residualSpeed = rb.linearVelocity-(rb.linearVelocity * dir);
        float acutalSpeed = rb.linearVelocity.magnitude;
        rb.linearVelocity = (dir * (acutalSpeed >= impulseForce ? acutalSpeed : impulseForce))+residualSpeed;
    }
    void animUnabled()
    {
        myAnim.Rebind();
        myAnim.enabled = false;
    }
}
