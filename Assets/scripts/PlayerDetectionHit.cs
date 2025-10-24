using System;
using System.Linq;
using UnityEngine;

public class PlayerDetectionHit : MonoBehaviour
{
    [SerializeField] private AudioClip deadSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si colisionamos con la zona de muerte "DeadZone" o colisionamos con un enemigo
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            AudioManager.Instance.PlaySFX(deadSFX, 1f);
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        
        GameObject.Find("GameManager").GetComponent<GameManager>().restartValues();
    }
}
