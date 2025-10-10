
using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entered");
        // Si colisionamos con la zona de muerte "DeadZone" o colisionamos con un enemigo
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerDetectionHit>().SpawnPlayer();
        }
    }
}
