using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float scrollSensitivity = 2f;
    public GameObject[] slimes;
    public int[] amounts;
    private int[] currentAmounts;
    public playerShoot player;
    public slimeRow row;
    int currentIndex = 0;
    Animator fadeOverlay;
    private GameObject spawnPoints;
    private Transform[] spawnPointArray;
    Transform lastSpawnTransform;
    void Start()
    {
        player.gm = this;
        fadeOverlay = GameObject.Find("fadeOverlay").GetComponent<Animator>();
        spawnPoints = GameObject.FindGameObjectWithTag("SpawnPoint");
        spawnPointArray = spawnPoints.GetComponentsInChildren<Transform>();
        String lastPoint = PlayerPrefs.GetString("LastSpawnPoint", "");
        lastSpawnTransform = spawnPointArray[0];
        foreach (var point in spawnPointArray)
        {
            if (point.name == lastPoint)
            {
                lastSpawnTransform = point;
                break;
            }
        }
        //player.transform.position = lastSpawnTransform.position;
        GameObject.Find("Main Camera").GetComponent<cameraFollow>().setPosition(lastSpawnTransform.position);
        restartValues();
        PlayerPrefs.DeleteKey("LastSpawnPoint");
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0 && Mathf.Abs(scroll) < scrollSensitivity)
        {
            scrollSlime(scroll<0?-1:1);
        }
    }

    void scrollSlime(int direction)
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
        player.transform.position = lastSpawnTransform.position;
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
    public void fadeAnimation()
    {
        fadeOverlay.Play("fade");
    }
}
