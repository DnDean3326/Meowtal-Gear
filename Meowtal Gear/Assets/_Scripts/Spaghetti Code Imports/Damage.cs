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
        if (other.CompareTag("Player"))
        {
            print("I dealt damage!");
            player.Damage(damageDealt, damageForce);
            //this will knock back the player if colliding with the damaging object.
            player.knockbackCount = player.knockbackLength;
            //if the x.position of the damage is to your right, the player will get knocked back to the left.
            if (other.transform.position.x < transform.position.x) {
                player.knockFromRight = true;
            } else {
                player.knockFromRight = false;
            }
        }
    }
}
