using System;
using System.Collections;
using UnityEngine;

public class RovingEnemy : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myBoxCollider;
    private Animator myAnimator;
    [SerializeField] private float moveSpeed;
    private float currentSpeed;
    [SerializeField] private float waitDuration;
    private bool isWaiting;
    private bool flipDelay;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        isWaiting = false;
        currentSpeed = moveSpeed;
        StartCoroutine(FlipDelayPeriod(0.25f));
    }
    
    private void Update()
    {
        AnimationControl();
        Movement();
    }

    private void AnimationControl()
    {
        myAnimator.SetBool("isWaiting", isWaiting);
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
        if (!other.gameObject.CompareTag("Player") && !flipDelay) {
            FlipDirection();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (flipDelay) return;
        isWaiting = true;
        StartCoroutine(WaitForTime(waitDuration));
    }
    
    IEnumerator WaitForTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        FlipDirection();
    }

    IEnumerator FlipDelayPeriod(float duration)
    {
        flipDelay = true;
        yield return new WaitForSeconds(duration);
        flipDelay = false;
    }

    private void FlipDirection()
    {
        StartCoroutine(FlipDelayPeriod(1f));
        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(transform.localScale.x * -1, 1f);
        isWaiting = false;
    }
}
