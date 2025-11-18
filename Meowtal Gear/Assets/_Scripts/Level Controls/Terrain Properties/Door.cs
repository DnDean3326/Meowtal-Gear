using UnityEngine;

public class Door : MonoBehaviour
{
    private LevelManager theLevelManager; 
    private SpriteRenderer mySpriteRenderer;
    private BoxCollider2D myBoxCollider2D;

    private void Awake()
    {
        theLevelManager = FindFirstObjectByType<LevelManager>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }
        
    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name + " collided with " + gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            if (theLevelManager.GetKeyCount() >= 1)
            {
                print(theLevelManager.GetKeyCount());
                theLevelManager.AddKeys(-1);
                mySpriteRenderer.enabled = false;
                myBoxCollider2D.enabled = false;
            }
        }
    }
}
