using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] slimes;
    public int[] amounts;
    public playerShoot player;
    int currentIndex = 0;
    void Start()
    {
        player.setCharge(slimes[currentIndex]);
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll > 0f)
        {
            ScrollSlime(1); // Scroll up
        }
        else if (scroll < 0f)
        {
            ScrollSlime(-1); // Scroll down
        }
    }

    void ScrollSlime(int direction)
    {
        // Wrap around using modulo
        currentIndex = (currentIndex + direction + slimes.Length) % slimes.Length;

        player.setCharge(slimes[currentIndex]);
        Debug.Log("Switched to slime index: " + currentIndex);
    }
}
