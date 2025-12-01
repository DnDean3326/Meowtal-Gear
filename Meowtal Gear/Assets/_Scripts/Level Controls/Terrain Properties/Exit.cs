using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private Animator myAnimator;
    private AudioSource source;
    [SerializeField] private AudioClip unlockSound;
    private bool hasTriggered = false;
    
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().CompareTag("Player") && !hasTriggered) {
            hasTriggered = true;
            source.PlayOneShot(unlockSound, 1F);
            transform.position = new Vector2(transform.position.x - 0.7f, transform.position.y);
            myAnimator.SetBool("IsUnlocked", true);
            StartCoroutine(WaitForTime(1F));
        }
    }
    
    IEnumerator WaitForTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
