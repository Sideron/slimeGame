using UnityEngine;

public class playerShoot : MonoBehaviour
{
    private GameObject slimeCharge;
    public float projectileSpeed = 10f;
    GunAim ga;
    void Start()
    {
        ga = GetComponentInChildren<GunAim>();
    }

    void Update()
    {
        // Ejemplo: dispara con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Asegúrate de que GunAim te dé la posición y dirección de disparo
        Vector2 shootPos = ga.getShootPosition();   // posición desde donde disparar
        Vector2 shootDir = ga.getShootDirection();  // dirección hacia donde disparar

        // Crear el proyectil
        GameObject proj = Instantiate(slimeCharge, shootPos, Quaternion.identity);

        // Aplicar velocidad
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDir.normalized * projectileSpeed;
        }
    }
    public void setCharge(GameObject slime)
    {
        slimeCharge = slime;
    }
}