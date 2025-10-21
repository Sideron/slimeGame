using System;
using System.Linq;
using UnityEngine;

public class PlayerDetectionHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si colisionamos con la zona de muerte "DeadZone" o colisionamos con un enemigo
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        
        GameObject.Find("GameManager").GetComponent<GameManager>().restartValues();
    }
}
