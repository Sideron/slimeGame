using UnityEngine;

public enum CameraDirection
{
    Horizontal,
    Vertical
}
public class cameraFollow : MonoBehaviour
{
    public Transform targetPoint;
    public float min = -5;
    public float max = 5;
    [SerializeField]
    private CameraDirection direction;
    public float speed = 10;
    void FixedUpdate()
    {
        Vector2 nPosition = Vector2.Lerp(transform.position, targetPoint.position, speed * Time.deltaTime);
        if (direction == CameraDirection.Horizontal)
        {
            transform.position = new Vector3(Mathf.Clamp(nPosition.x,min,max), transform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(nPosition.y,min,max), -10);
        }
    }
}
