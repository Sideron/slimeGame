using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrgNextLevel : MonoBehaviour
{
    [SerializeField]
    private int targetLevel = 0;
    [SerializeField] private String spawnPointName = "";
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>().fadeAnimation();
            PlayerPrefs.SetString("LastSpawnPoint", spawnPointName);
            Invoke("goTargetLevel", 0.45f);
        }
    }

    private void goTargetLevel()
    {
        
        SceneManager.LoadScene(targetLevel);
    }
}
