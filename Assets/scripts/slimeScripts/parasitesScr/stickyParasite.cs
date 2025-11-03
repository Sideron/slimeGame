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
            sticky.releasePlayer();
            pc.Dash();
            Destroy(this);
        }
    }
}
