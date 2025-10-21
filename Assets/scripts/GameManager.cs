using System;
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
        restartValues();
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
    }
    public bool canShoot(){ return currentAmounts[currentIndex] > 0; }
    public void reduceSlimeCount()
    {
        if (canShoot())
        {
            currentAmounts[currentIndex] = currentAmounts[currentIndex] - 1;
            row.updateCounter(currentIndex, currentAmounts[currentIndex]);
        }
    }
    public void restartValues()
    {
        Debug.Log("Respawn");
        GameObject spawn = GameObject.FindGameObjectWithTag("SpawnPoint");
        player.transform.localPosition = spawn.transform.localPosition;
        GameObject[] slimesInScene = GameObject.FindGameObjectsWithTag("Slime");

        foreach (var slime in slimesInScene)
        {
            Destroy(slime);
        }
        currentIndex = 0;
        row.setIndex(currentIndex);
        currentAmounts = new int[amounts.Length];
        Array.Copy(amounts, currentAmounts, amounts.Length);
        player.setCharge(slimes[currentIndex]);
        row.updateRow(currentAmounts);
    }
}
