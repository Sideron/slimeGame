using System.Linq;
using UnityEngine;

public class checkExtract : MonoBehaviour
{
    [SerializeField]private Slime[] slimes;
    void OnTriggerEnter2D(Collider2D collision)
    {

        Slime isSlime = collision.transform.GetComponent<Slime>();
        Debug.Log(isSlime);
        if (isSlime)
        {
            slimes.Append(isSlime);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Slime exited");
        Slime isSlime = collision.GetComponent<Slime>();
        if (isSlime)
        {
            slimes = slimes.Where(s => s != isSlime).ToArray();
        }
    }
}
