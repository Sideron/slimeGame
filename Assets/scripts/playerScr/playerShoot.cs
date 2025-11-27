using Unity.VisualScripting;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    private GameObject slimeCharge;
    public float projectileSpeed = 10f;
    public GameManager gm;
    public LayerMask layerMask;
    private TrajectoryLine trajectoryLine;
    [SerializeField]
    private Animator shootAnimator;
    GunAim ga;

    [SerializeField] private AudioClip[] shootSFX;

    void Start()
    {
        ga = GetComponentInChildren<GunAim>();
        trajectoryLine = GetComponentInChildren<TrajectoryLine>();
        trajectoryLine.launchSpeed = projectileSpeed;
        setCharge(slimeCharge);
    }

    void Update()
    {
        Vector2 myPosition = new Vector2(transform.position.x, transform.position.y);
        //trajectoryLine.launchPoint = ga.getShootPosition() - myPosition;
        Vector2 dir = ga.getShootPosition() - myPosition;
        trajectoryLine.angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        trajectoryLine.DrawTrajectory();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (gm.canShoot())
        {
            shootAnimator.Rebind();
            shootAnimator.Play("shootAnim", -1, 0f);
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
            AudioManager.Instance.PlayRandomSFX(shootSFX);
            gm.reduceSlimeCount();
        }
    }
    public void setCharge(GameObject slime)
    {
        Rigidbody2D rb = slime.GetComponent<Rigidbody2D>();
        if (rb != null && trajectoryLine != null)
        {
            trajectoryLine.setGravity(rb.gravityScale*rb.mass);
        }
        slimeCharge = slime;
    }
}