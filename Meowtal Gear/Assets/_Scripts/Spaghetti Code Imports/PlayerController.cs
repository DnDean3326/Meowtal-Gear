using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    
    // Variable Declarations
    private Vector2 moveInput;
    
    // Movement Stats
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Vector2 wallJumpPower;
    [SerializeField] private float wallJumpDuration;
    [SerializeField] private float wallJumpDirection;
    [SerializeField] private float slideSpeed;
    private bool isWallJumping = false;

    // Ground check for Jump
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius; // Radius of groundcheck
    [SerializeField] private LayerMask whatIsGround; // What layer can the player jump
    [SerializeField] private bool isGrounded; // Is the player grounded?

    // Wall check for Wall Jump
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius; // Radius of wallcheck
    [SerializeField] private LayerMask whatIsWall; // What layer can the player wall slide/jump
    [SerializeField] private bool isWalled; // Is the player on the wall?
    
    // DMG knockback
    public float knockbackLength; //how long we are going to get pushed back
    public float knockbackCount;
    public bool knockFromRight; //direction where we are going to get pushed

    // HP
    [SerializeField]  private int maxHealth = 5;
    public int currentHealth;
    
    // Audio
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip doubleJumpSound;
    [SerializeField] AudioClip wallJumpSound;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip zoomSound;
    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip hurtSound;
    private AudioSource source;
    
    private LevelManager theLevelManager;

    private bool doubleJump = false;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        theLevelManager = FindObjectOfType<LevelManager>();
        myAnimator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        myAnimator.SetFloat("Speed", Mathf.Abs(myRigidBody.linearVelocity.x));
        myAnimator.SetBool("Grounded", isGrounded);

        isWalled = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isWalled && isWallJumping != true && isGrounded != true) {
            wallJumpDirection = -transform.localScale.x;
        }
        if (isGrounded) {
            doubleJump = true;
        }

        if (currentHealth > maxHealth) {  currentHealth = maxHealth; }
        if (currentHealth <= 0) { Die();}
        Run();
        FlipSprite();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded) {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.y, jumpPower);
            source.PlayOneShot(jumpSound, 1F);
        } else if (value.isPressed && isWalled && isWallJumping != true) {
            isWallJumping = true;
            myRigidBody.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            source.PlayOneShot(wallJumpSound, 1F);
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.linearVelocity.x), 1f);
            Invoke(nameof(StopWallJumping), wallJumpDuration);
        } else if (value.isPressed && doubleJump) {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.y, jumpPower);
            source.PlayOneShot(doubleJumpSound, 1F);
            doubleJump = false;
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Run()
    {
        if (!isWallJumping) {
            myRigidBody.linearVelocity = new Vector2((moveInput.x * moveSpeed), myRigidBody.linearVelocity.y);
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

    private void OnCollisionStay2D(Collision2D other)
    {
        if (isWalled && isWallJumping != true && isGrounded != true)
        {
            wallJumpDirection = -transform.localScale.x;
            if (other.transform.CompareTag("Jump Through Platform")) {
                //
            } else {
                myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, -slideSpeed);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Moving Platform"))
        {
            transform.parent = other.transform;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Moving Platform"))
        {
            transform.parent = null;
        }
    }

    public void Damage(int dmg, int knockback)
    {
        currentHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Character_Redflash");
        source.PlayOneShot(hurtSound, 1F);
        DamageReact(knockback);
    }
    
    public void Heal(int heal)
    {
        currentHealth += heal;
        gameObject.GetComponent<Animation>().Play("Character_Greenflash");
    }

    private void DamageReact(int force)
    {
        if (knockFromRight) {
            myRigidBody.linearVelocity = new Vector2(-force, force);
        }
        if (!knockFromRight) {
            myRigidBody.linearVelocity = new Vector2(force, force); 
            knockbackCount -= Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer ("Kill Plane")) {
            Die();
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
        } else if (collectible.GetCollectibleType() == "heart" && currentHealth < 5) {
            int healValue = (int)collectible.GetValueChange();
            source.PlayOneShot(healSound, 1F);
            Heal(healValue);
            return false;
        } else { return true;}
    }
    
    public void DeactivateCollectible(CollectibleSO collectible)
    {
        if (collectible.GetCollectibleType() == "speed")
        {
            moveSpeed -= collectible.GetValueChange();
        }
    }
}
