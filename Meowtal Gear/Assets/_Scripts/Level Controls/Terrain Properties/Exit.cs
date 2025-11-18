using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private int LevelToLoad;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().CompareTag("Player")){
            SceneManager.LoadScene(LevelToLoad);
        }
    }
}
