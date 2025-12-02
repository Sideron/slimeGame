using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class radialEffect : MonoBehaviour
{
    [SerializeField]
    public Sprite[] images;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = images[0];
    }

    public void setValue(float value)
    {
        spriteRenderer.sprite = images[(int)(Mathf.Clamp01(value)*(images.Length-1))];
    }
}
