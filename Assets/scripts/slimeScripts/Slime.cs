using UnityEngine;
using System.Collections;

public abstract class Slime : MonoBehaviour
{
    public bool IsBeingExtracted { get; private set; } = false;

    public virtual void onTouch(Rigidbody2D rb) { }
    public virtual void onRelease(Rigidbody2D rb) { }
    public virtual void onShoot(Rigidbody2D rb) { }

    public void onExtract(Transform player, GameObject particlePrefab = null)
    {
        if (IsBeingExtracted) return; // evita extracción múltiple
        IsBeingExtracted = true;

        // Efecto de partículas si existe prefab
        if (particlePrefab != null)
        {
            GameObject psObj = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = psObj.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                var main = ps.main;
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                    main.startColor = sr.color;
            }

            Destroy(psObj, 2f); // destruye el objeto después de un tiempo
        }

        StartCoroutine(AbsorbAnimation(player));
    }

    private IEnumerator AbsorbAnimation(Transform player)
    {
        float duration = 0.5f;
        float time = 0f;

        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color startColor = sr.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // Mover hacia el jugador
            transform.position = Vector3.Lerp(startPos, player.position, t);

            // Encoger
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            // Desvanecer color
            sr.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0f), t);

            yield return null;
        }

        Destroy(gameObject);
    }
}
