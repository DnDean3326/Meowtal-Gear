using System;
using System.Collections;
using UnityEngine;

public class RovingEnemy : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myBoxCollider;
    [SerializeField] private float moveSpeed;
    private float currentSpeed;
    [SerializeField] private float waitDuration;
    private bool isWaiting;
    private bool recentBonk;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }
    
    private void Start()
    {
        isWaiting = false;
        currentSpeed = moveSpeed;
    }
    
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!isWaiting) {
            myRigidbody.linearVelocity = new Vector2(moveSpeed, 0f);
        }

        if (isWaiting){
            myRigidbody.linearVelocity = new Vector2(0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            recentBonk = true;
            FlipDirection();
            StartCoroutine(BonkCooldown(1.5f));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!recentBonk) {
            isWaiting = true;
            StartCoroutine(WaitForTime(waitDuration));
        }
    }
    
    IEnumerator WaitForTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        FlipDirection();
    }
    
    IEnumerator BonkCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        recentBonk = false;
    }

    private void FlipDirection()
    {
        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(transform.localScale.x * -1, 1f);
        isWaiting = false;
    }
}
