using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] int damageDealt;
    [SerializeField] int damageForce;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            bool direction;
            if (other.transform.position.x < transform.position.x) {
                direction = true;
            } else {
                direction = false;
            }
            player.Damage(damageDealt, damageForce, direction);
        }
    }
}
