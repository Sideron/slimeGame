using UnityEngine;

public class playerShoot : MonoBehaviour
{
    private GameObject slimeCharge;
    public float projectileSpeed = 10f;
    public GameManager gm;
    GunAim ga;

    [SerializeField] private AudioClip shootSFX;

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
        if (gm.canShoot())
        {
            // Asegúrate de que GunAim te dé la posición y dirección de disparo
            RaycastHit2D rayCollision = Physics2D.Raycast(transform.position, ga.getShootPosition());
            Vector2 shootDir = ga.getShootDirection();  // dirección hacia donde disparar
            Vector2 shootPos = (rayCollision.distance*shootDir)+ new Vector2(transform.position.x,transform.position.y);   // posición desde donde disparar

            GameObject proj = Instantiate(slimeCharge, shootPos, Quaternion.identity);

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = shootDir.normalized * projectileSpeed;
            }
            proj.GetComponent<Slime>().onShoot(GetComponent<Rigidbody2D>());
            AudioManager.Instance.PlaySFX(shootSFX);
            gm.reduceSlimeCount();
        }
    }
    public void setCharge(GameObject slime)
    {
        slimeCharge = slime;
    }
}