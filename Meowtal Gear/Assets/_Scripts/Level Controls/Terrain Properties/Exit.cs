using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Animator myAnimator;
    private AudioSource source;
    [SerializeField] private AudioClip unlockSound;
    
    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().CompareTag("Player")){
            source.PlayOneShot(unlockSound, 1F);
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
