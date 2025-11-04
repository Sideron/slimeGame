using Unity.VisualScripting;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    private GameObject slimeCharge;
    public float projectileSpeed = 10f;
    public GameManager gm;
    public LayerMask layerMask;
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
            Vector2 myPosition = new Vector2(transform.position.x, transform.position.y);
            float distance = (-myPosition + ga.getShootPosition()).magnitude;
            RaycastHit2D rayCollision = Physics2D.Raycast(myPosition, -myPosition + ga.getShootPosition(),distance,layerMask);
            Vector2 shootDir = (-myPosition + ga.getShootPosition()).normalized;
            Vector2 shootPos;
            if (rayCollision.collider != null)
            {
                shootPos = (rayCollision.distance * shootDir) + myPosition;
            }
            else
            {
                shootPos = myPosition + shootDir * distance; // dispara a una distancia fija si no choca
            }
            GameObject proj = Instantiate(slimeCharge, shootPos, Quaternion.identity);

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                //rb.linearVelocity = shootDir.normalized * projectileSpeed;
                rb.AddForce(shootDir.normalized * projectileSpeed, ForceMode2D.Impulse);
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