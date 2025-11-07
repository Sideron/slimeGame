using Unity.VisualScripting;
using UnityEngine;

public class StickySlime : Slime
{
    [SerializeField] private GameObject stickedObject;
    Rigidbody2D myrb;
    bool wallSticked = false;
    [SerializeField] private float viscosity = 50f;
    [SerializeField] private AudioClip sticky;
    [SerializeField] private AudioClip stickyRelease;
    [SerializeField] private ParticleSystem stickyParticles;
    void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
        stickyParticles.Stop();
    }
    public override void onTouch(Rigidbody2D rb)
    {
        if (wallSticked && stickedObject == null)
        {
            AudioManager.Instance.PlaySFX(sticky, 1.0f);
            stickyParticles.Stop();
            stickyParticles.Play();
            Debug.Log("Entered");
            rb.bodyType = RigidbodyType2D.Static;
            stickedObject = rb.gameObject;
            playerController player = rb.transform.GetComponent<playerController>();
            if (player != null)
            {
                stickyParasite sp = player.transform.AddComponent<stickyParasite>();
                sp.sticky = this;
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
            if (player != null)
            {
                releasePlayer(rb);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        myrb.bodyType = RigidbodyType2D.Static;
        wallSticked = true;
        stickyParticles.Stop();
        stickyParticles.Play();
    }
    public void releasePlayer(Rigidbody2D playerRB)
    {
        AudioManager.Instance.PlaySFX(stickyRelease, 0.6f);
        playerRB.bodyType = RigidbodyType2D.Dynamic;
        playerRB.linearDamping = 0;
        stickedObject = null;
    }
}
