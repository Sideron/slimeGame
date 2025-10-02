using UnityEngine;

public abstract class Slime : MonoBehaviour
{
    public abstract void onTouch(Rigidbody2D rb);
    public abstract void onRelease(Rigidbody2D rb);
}
