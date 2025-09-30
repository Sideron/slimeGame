using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform followPoint;
    public float speed = 10;
    void FixedUpdate()
    {
        Vector2 nPosition = Vector2.Lerp(transform.position,followPoint.position,speed*Time.deltaTime);
        transform.position = new Vector3(nPosition.x, nPosition.y,-10);
    }
}
