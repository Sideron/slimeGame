using UnityEngine;

public class TeleportSlime : Slime
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Destroyed");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.transform.position = transform.position;
        Destroy(gameObject);
    }
}
