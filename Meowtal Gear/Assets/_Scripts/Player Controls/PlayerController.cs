using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private LevelManager theLevelManager; 
    [SerializeField] private CapsuleCollider2D bodyBox;
    [SerializeField] private BoxCollider2D feetBox;
    [SerializeField] private BoxCollider2D handBox;
    
    // Variable Declarations
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isWalled;
    private bool doubleJump;
    private bool isWallJumping;
    private float initialGravity;
    private float maxHealth = 100;
    private float currentHealth; // Build a method to pull this so the value can remain private
    private Vector2 moveInput;
    
    // Movement Stats
    [SerializeField] private float moveSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Vector2 wallJumpPower;
    [SerializeField] private float wallJumpDuration;
    private float wallJumpDirection;

    // DMG knockback
    private float knockbackLength;
    private bool knockFromRight;
    private bool immuneToDamage;
    
    // Audio
    private AudioSource source;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip doubleJumpSound;
    [SerializeField] AudioClip wallJumpSound;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip zoomSound;
    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip hurtSound;
    
    private void Awake()
    {
        theLevelManager = FindFirstObjectByType<LevelManager>();
        myRigidBody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        initialGravity = myRigidBody.gravityScale;
        StartCoroutine(DamageImmunity(0.25f));
    }

    // Update is called once per frame
    private void Update()
    {
        HealthManage();
        JumpManage();
        isGrounded = feetBox.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Run();
        Climb();
        FlipSprite();
    }

    private void HealthManage()
    {
        if (currentHealth > maxHealth) {  currentHealth = maxHealth; }
        if (currentHealth <= 0) { Die();}
    }

    private void JumpManage()
    {
        if (isWalled && !isWallJumping && !isGrounded) {
            wallJumpDirection = -transform.localScale.x;
        }
        if (isGrounded) {
            doubleJump = true;
        }
    }
    
    private void Run()
    {
        if (!isWallJumping) {
            Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.linearVelocity.y);
            myRigidBody.linearVelocity = playerVelocity;
            bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
            //myAnimator.SetBool("IsRunning", hasHorizontalSpeed);
        } 
    }

    private void Climb()
    {
        if (feetBox.IsTouchingLayers(LayerMask.GetMask("Climbable")))
        {
            Vector2 playerClimbVelocity = new Vector2(myRigidBody.linearVelocity.x, moveInput.y * climbSpeed);
            myRigidBody.linearVelocity = playerClimbVelocity;
            //myAnimator.SetBool("IsClimbing", true);
            myRigidBody.gravityScale = 0;
        } else {
            myRigidBody.gravityScale = initialGravity;
            //myAnimator.SetBool("IsClimbing", false);
        }
    }

    private void FlipSprite()
    {
        if (!isWallJumping) {
            bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
            if (hasHorizontalSpeed)
            {
                transform.localScale = new Vector2(Mathf.Sign(myRigidBody.linearVelocity.x), 1f);
            }
        }
    }
    
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded) {
            myRigidBody.linearVelocity = Vector2.up * jumpPower;
            source.PlayOneShot(jumpSound, 1F);
        } else if (value.isPressed && isWalled && isWallJumping != true) {
            isWallJumping = true;
            myRigidBody.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y); // TODO Match this code to the * jumpPower format
            source.PlayOneShot(wallJumpSound, 1F);
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.linearVelocity.x), 1f);
            Invoke(nameof(StopWallJumping), wallJumpDuration);
        } else if (value.isPressed && doubleJump) {
            myRigidBody.linearVelocity = Vector2.up * jumpPower;
            source.PlayOneShot(doubleJumpSound, 1F);
            doubleJump = false;
        }
    }
    
    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Moving")) {
            transform.parent = other.transform;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Lethal")) {
            Die();
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (isWalled && !isWallJumping && !isGrounded)
        {
            wallJumpDirection = -transform.localScale.x;
            if (other.transform.CompareTag("Jump Through")) {
                //
            } else {
                myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, -slideSpeed);
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Moving")) {
            transform.parent = null;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (handBox.IsTouching(col) && col.CompareTag("Wall")) {
            isWalled = true;
        }
        
        if (col.gameObject.layer == LayerMask.NameToLayer ("Lethal")) {
            Die();
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Wall")) {
            isWalled = false;
        }
    }

    public float GetHealth()
    {
        return  currentHealth;
    }

    public float GetMaxHealth()
    {
        return  maxHealth;
    }

    public void HealthChange(int changeAmount, int knockback, bool direction)
    {
        if (changeAmount < 0 && !immuneToDamage) {
            currentHealth += changeAmount;
            //gameObject.GetComponent<Animation>().Play("Character_Redflash");
            source.PlayOneShot(hurtSound, 1F);
            DamageReact(knockback, direction);
            StartCoroutine(DamageImmunity(0.25f));
        } else if (changeAmount > 0) {
            currentHealth += changeAmount;
            //gameObject.GetComponent<Animation>().Play("Character_Greenflash");
        }
    }

    
    private void DamageReact(int force, bool direction)
    {
        if (direction) {
            myRigidBody.linearVelocity = new Vector2(-force, force);
        }
        if (!direction) {
            myRigidBody.linearVelocity = new Vector2(force, force); 
        }
    }
    
    void Die()
    {
        // add some particle effect down the line
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentHealth = maxHealth;
    }
    
    public bool ActivateCollectible(CollectibleSO collectible)
    {
        if (collectible.GetCollectibleType() == "speed") {
            moveSpeed += collectible.GetValueChange();
            source.PlayOneShot(zoomSound, 1F);
            return false;
        } else if (collectible.GetCollectibleType() == "coin") {
            int coinValue = (int)collectible.GetValueChange();
            source.PlayOneShot(coinSound, 1F);
            theLevelManager.AddCoins(coinValue);
            return false;
        } else if (collectible.GetCollectibleType() == "health" && currentHealth < 5) {
            int healValue = (int)collectible.GetValueChange();
            source.PlayOneShot(healSound, 1F);
            HealthChange(healValue, 0, false);
            return false;
        } else if (collectible.GetCollectibleType() == "key") {
            int keyValue = (int)collectible.GetValueChange();
            //source.PlayOneShot(healSound, 1F);
            theLevelManager.AddKeys(keyValue);
            return false;
        } else { return true; }
    }
    
    public void DeactivateCollectible(CollectibleSO collectible)
    {
        if (collectible.GetCollectibleType() == "speed")
        {
            moveSpeed -= collectible.GetValueChange();
        }
    }

    IEnumerator DamageImmunity(float duration)
    {
        immuneToDamage = true;
        yield return new WaitForSeconds(duration);
        immuneToDamage = false;
    }
}
