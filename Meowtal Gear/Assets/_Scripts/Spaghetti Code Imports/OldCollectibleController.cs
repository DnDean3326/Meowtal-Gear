using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField] CollectibleSO collectible;
    private PlayerController player;
    private SpriteRenderer spriteRenderer;
    private float timeLeft;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
    }
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CountdownTimer();
    }

    void CountdownTimer()
    {
        if (spriteRenderer.enabled == false && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                player.DeactivateCollectible(collectible);  
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && spriteRenderer.enabled) {
            // Activate collectible
            timeLeft = collectible.GetDuration();
            // Below method not only activates the collectible, but returns a bool letting the system know whether to disable the sprite
            bool visibility = player.ActivateCollectible(collectible);
            spriteRenderer.enabled = visibility;
        }
    }

    public void ChangeSpriteVisibility(bool visibility)
    {
        spriteRenderer.enabled = visibility;
    }
    
    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
}
