using UnityEngine;

public abstract class Slime : MonoBehaviour
{
    public virtual void onTouch(Rigidbody2D rb){}
    public virtual void onRelease(Rigidbody2D rb){}
    public virtual void onShoot(Rigidbody2D rb){}
}
