using UnityEngine;

public class TeleportSlime : Slime
{
    [SerializeField] private AudioClip teleportAudio;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Destroyed");
        AudioManager.Instance.PlaySFX(teleportAudio, 1.0f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.transform.position = transform.position;
        Destroy(gameObject);
    }
}
