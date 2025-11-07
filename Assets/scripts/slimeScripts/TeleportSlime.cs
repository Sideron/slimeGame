using UnityEngine;

public class TeleportSlime : Slime
{
    [SerializeField] private AudioClip teleportAudio;
    [SerializeField] private GameObject teleportEffectPrefab;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Destroyed");
        AudioManager.Instance.PlaySFX(teleportAudio, 1.0f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        Instantiate(teleportEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
