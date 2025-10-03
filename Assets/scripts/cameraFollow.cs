using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform targetPoint;
    public float speed = 10;
    void FixedUpdate()
    {
        Vector2 followPoint = targetPoint.transform.right*2 + targetPoint.transform.position;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(mouseWorld,targetPoint.transform.position) < 2)
        {
            followPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        Vector2 nPosition = Vector2.Lerp(transform.position,followPoint,speed*Time.deltaTime);
        //Vector2 nPosition = followPoint;
        transform.position = new Vector3(nPosition.x, nPosition.y,-10);
    }
}
