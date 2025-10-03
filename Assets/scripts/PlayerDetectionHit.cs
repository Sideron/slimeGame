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

    private void SpawnPlayer()
    {
        // Ubicamos el SpawnPoint, eso significa que el spawnpoint debe tener su etiqueta (tag)
        GameObject spawn = GameObject.FindGameObjectWithTag("SpawnPoint");
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");

        foreach (var slime in slimes)
        {
            Destroy(slime);
        }

        // Mandamos al player a esa posici�n.
        transform.localPosition = spawn.transform.localPosition;
    }
}
