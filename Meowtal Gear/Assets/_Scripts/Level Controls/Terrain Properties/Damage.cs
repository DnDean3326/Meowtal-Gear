using UnityEngine;

public class Damage : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] int damageDealt;
    [SerializeField] int damageForce;
    bool knockbackFromRight;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.position.x < transform.position.x) {
                knockbackFromRight = true;
            } else {
                knockbackFromRight = false;
            }
            player.HealthChange(-damageDealt, damageForce, knockbackFromRight);
        }
    }
}
