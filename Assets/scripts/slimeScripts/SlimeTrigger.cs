using UnityEngine;

public class SlimeTrigger : MonoBehaviour
{
    Slime slime;
    void Start(){
        slime = transform.parent.GetComponent<Slime>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.attachedRigidbody != null)
        {
            slime?.onTouch(other.attachedRigidbody);
        }
    }
}
