using UnityEngine;
public class cameraFollow : MonoBehaviour
{
    public Transform targetPoint;
    [Header("Camera Bounds")]
    public float minX = -5;
    public float maxX = 5;
    public float minY = -5;
    public float maxY = 5;
    [SerializeField]
    //private bool freezeY = true;
    public float speed = 10;
    public float differenceX = 3f;
    public float offsetFactor = 0.025f;

    private float pointerDiff = 0f;
    private float lastX = 0f;
    public void setPosition(Vector2 position)
    {
        transform.position = new Vector3(
            Mathf.Clamp(position.x, minX, maxX),
            Mathf.Clamp(position.y, minY, maxY),
            -10f
        );
    }
    void FixedUpdate()
    {
        float diff = targetPoint.position.x - lastX;
        float relative = (diff!=0? Mathf.Sign(diff):0) * offsetFactor;

        pointerDiff = Mathf.Clamp(pointerDiff + relative, -1f, 1f);

        lastX = targetPoint.position.x;

        float targetX = Mathf.Clamp(
            targetPoint.position.x + (differenceX * pointerDiff),
            minX,
            maxX
        );

        float targetY = Mathf.Clamp(
            targetPoint.position.y,
            minY,
            maxY
        );

        float smoothX = Mathf.Lerp(transform.position.x, targetX, speed * Time.deltaTime);

        transform.position = new Vector3(smoothX, targetY, -10f);
        /*if (freezeY)
        {
            transform.position = new Vector3(Mathf.Clamp(nPosition.x,min,max), transform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(nPosition.y,min,max), -10);
        }*/
    }
}
