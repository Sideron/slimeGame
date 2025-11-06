using UnityEngine;

public class stickyParasite : MonoBehaviour
{
    public StickySlime sticky;
    private playerController pc;
    void Start()
    {
        pc = GetComponent<playerController>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            sticky.releasePlayer(GetComponent<Rigidbody2D>());
            pc.Dash();
            Destroy(this);
        }
    }
}
