using UnityEngine;

public class SlimeTrigger : MonoBehaviour
{
    Slime slime;
    void Start()
    {
        slime = transform.parent.GetComponent<Slime>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody != null && !other.isTrigger)
        {
            slime?.onTouch(other.attachedRigidbody);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if (other.attachedRigidbody != null && !other.isTrigger)
        {
            slime?.onRelease(other.attachedRigidbody);
        }
    }
}
