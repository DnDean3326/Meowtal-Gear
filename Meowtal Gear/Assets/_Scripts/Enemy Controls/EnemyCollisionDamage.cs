using UnityEngine;

public class EnemyCollisionDamage : MonoBehaviour
{
    private PlayerController player;
    
    [SerializeField] int damageDealt;
    [SerializeField] int damageForce;
    bool knockbackFromRight;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            if (collision.transform.position.x < transform.position.x) {
                knockbackFromRight = true;
            } else {
                knockbackFromRight = false;
            }
            player.HealthChange(-damageDealt, damageForce, knockbackFromRight);
        }
    }
}
