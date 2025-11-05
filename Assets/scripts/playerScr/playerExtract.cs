using UnityEngine;

public class playerExtract : MonoBehaviour
{
    //private checkExtract ce;
    private GunAim ga;
    [SerializeField] private float extractDistance = 10f;
    [SerializeField] private LayerMask slimeLayer;
    private GameObject particleEffect;
    void Start()
    {
        //ce = GetComponentInChildren<checkExtract>();
        ga = GetComponentInChildren<GunAim>();
        particleEffect = GameObject.Find("extractEffect");
        particleEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            particleEffect.SetActive(true);
            Vector2 direction = ga.getShootPosition() - new Vector2(transform.position.x, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, extractDistance, slimeLayer);
            Debug.Log(hit.collider);
            if (hit.collider != null)
            {
                Slime isSlime = hit.collider.GetComponent<Slime>();
                if (isSlime)
                {
                    isSlime.onExtract();
                }
            }
        }
        else
        {
            particleEffect.SetActive(false);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = ga.getShootPosition() - new Vector2(transform.position.x, transform.position.y);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction.normalized * extractDistance);
    }
}
