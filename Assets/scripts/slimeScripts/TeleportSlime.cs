using UnityEngine;

public class TeleportSlime : Slime
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Destroyed");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
        Destroy(gameObject);
    }
}
