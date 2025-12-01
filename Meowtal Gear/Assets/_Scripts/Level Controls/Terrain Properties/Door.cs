using UnityEngine;

public class Door : MonoBehaviour
{
    private LevelManager theLevelManager; 
    private BoxCollider2D myBoxCollider2D;
    private Animator myAnimator;
    private AudioSource source;
    [SerializeField] private AudioClip unlockSound;
    [SerializeField] private bool isFacingRight;

    private void Awake()
    {
        theLevelManager = FindFirstObjectByType<LevelManager>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        source = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
    }
        
    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name + " collided with " + gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            if (theLevelManager.GetKeyCount() >= 1)
            {
                theLevelManager.AddKeys(-1);
                source.PlayOneShot(unlockSound, 1F);
                myAnimator.SetBool("IsUnlocked", true);
                transform.position = isFacingRight ? new Vector2(transform.position.x + -0.7f, transform.position.y) : new Vector2(transform.position.x + 0.7f, transform.position.y);
                myBoxCollider2D.enabled = false;
            }
        }
    }
}
