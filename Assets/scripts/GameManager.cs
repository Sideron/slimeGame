using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] slimes;
    public int[] amounts;
    private int[] currentAmounts;
    public playerShoot player;
    public slimeRow row;
    int currentIndex = 0;
    void Start()
    {
        player.gm = this;
        currentAmounts = amounts;
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
        row.setIndex(currentIndex);
        Debug.Log("Switched to slime index: " + currentIndex);
    }
    public bool canShoot(){ return currentAmounts[currentIndex] > 0; }
    public void reduceSlimeCount()
    {
        if (canShoot())
        {
            currentAmounts[currentIndex] = currentAmounts[currentIndex] - 1;
        }
    }
}
