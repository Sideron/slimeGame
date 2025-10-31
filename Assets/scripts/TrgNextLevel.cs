using UnityEngine;
using UnityEngine.SceneManagement;

public class TrgNextLevel : MonoBehaviour
{
    [SerializeField]
    private int targetLevel = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetLevel);
        }
    }
}
