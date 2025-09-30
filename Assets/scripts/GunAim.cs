using UnityEngine;

public class GunAim : MonoBehaviour
{
    public float angleOffset = 0f; // corrige si tu sprite no apunta a la derecha por defecto
    SpriteRenderer gunSpr;
    [SerializeField]
    private Transform shootPosition;

    void Start() {
        gunSpr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        AimAtMouse();
    }

    void AimAtMouse()
    {
        // Posición del mouse en el mundo
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f; // mantener en 2D

        gunSpr.flipY = mouseWorld.x < transform.position.x;

        // Dirección desde la pistola hacia el mouse
        Vector3 dir = mouseWorld - transform.position;

        // Calcular ángulo en grados
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Aplicar rotación al objeto (solo a la pistola)
        transform.rotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
    }
    public Vector2 getShootPosition(){
        return shootPosition.position;
    }
    public Vector2 getShootDirection(){
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        return (mouseWorld - shootPosition.position).normalized;
    }
}
