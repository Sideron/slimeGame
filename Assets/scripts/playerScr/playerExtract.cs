using UnityEngine;

public class playerExtract : MonoBehaviour
{
    private GunAim ga;
    private GameManager gm;
    [SerializeField] private float extractDistance = 10f;
    [SerializeField] private LayerMask slimeLayer;
    [SerializeField] private GameObject extractParticlePrefab; // prefab de partículas opcional
    private GameObject particleEffect;
    bool isExtracting = false;

    void Start()
    {
        ga = GetComponentInChildren<GunAim>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        particleEffect = GameObject.Find("extractEffect");
        if (particleEffect != null)
            particleEffect.SetActive(false);
    }

    void Update()
    {
        // Disparo de extracción sostenido
        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0) && isExtracting)
        {
            Vector2 direction = ga.getShootPosition() - new Vector2(transform.position.x, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, extractDistance, slimeLayer);

            if (hit.collider != null)
            {
                Slime isSlime = hit.collider.GetComponent<Slime>();
                if (isSlime)
                {
                    extractSlime(isSlime);
                }
            }
        }

        // Inicia la animación y efectos visuales al hacer clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            if (particleEffect != null)
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
            if (particleEffect != null)
                particleEffect.SetActive(false);
            stopExtracting();
        }
    }

    void extractSlime(Slime slime)
    {
        // Evita sumar más de una vez por el mismo slime
        if (slime.IsBeingExtracted) return;

        gm.addSlimeToInventory(slime);
        slime.onExtract(transform, extractParticlePrefab);
    }

    void startExtracting()
    {
        isExtracting = true;
    }

    void stopExtracting()
    {
        isExtracting = false;
    }
}
