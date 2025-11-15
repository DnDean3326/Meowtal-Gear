using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private BoxCollider2D boxCollider;
    [SerializeField] private float fallDelay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        myRigidBody.bodyType = RigidbodyType2D.Dynamic;
        boxCollider.isTrigger = true;

        yield return 0;
    }
}
