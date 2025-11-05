using UnityEngine;

public class playerExtract : MonoBehaviour
{
    //private checkExtract ce;
    private GunAim ga;
    private GameManager gm;
    [SerializeField] private float extractDistance = 10f;
    [SerializeField] private LayerMask slimeLayer;
    private GameObject particleEffect;
    bool isExtracting = false;
    void Start()
    {
        //ce = GetComponentInChildren<checkExtract>();
        ga = GetComponentInChildren<GunAim>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        particleEffect = GameObject.Find("extractEffect");
        particleEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0) && isExtracting)
        {
            Vector2 direction = ga.getShootPosition() - new Vector2(transform.position.x, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, extractDistance, slimeLayer);
            Debug.Log(hit.collider);
            if (hit.collider != null)
            {
                Slime isSlime = hit.collider.GetComponent<Slime>();
                if (isSlime)
                {
                    extractSlime(isSlime);
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            particleEffect.SetActive(true);
            extractionEffect();
            Invoke("startExtracting", 0.4f);
        }
    }
    void extractionEffect()
    {
        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            Invoke("extractionEffect", 0.2f);
        }
        else
        {
            particleEffect.SetActive(false);
            stopExtracting();
        }
    }
    void extractSlime(Slime slime)
    {
        gm.addSlimeToInventory(slime);
        slime.onExtract();
    }
    void startExtracting()
    {
        isExtracting = true;
    }
    void stopExtracting()
    {
        isExtracting = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = ga.getShootPosition() - new Vector2(transform.position.x, transform.position.y);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction.normalized * extractDistance);
    }
}
