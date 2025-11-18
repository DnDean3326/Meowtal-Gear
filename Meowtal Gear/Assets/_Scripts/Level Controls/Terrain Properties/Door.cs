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
        
    private void OnCollisionEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (theLevelManager.GetKeyCount() > 1)
            {
                theLevelManager.SubtractKeys(1);
                mySpriteRenderer.enabled = false;
                myBoxCollider2D.enabled = false;
            }
        }
    }
}
