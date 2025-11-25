using System;
using System.Collections;
using UnityEngine;

public class EnemyDamageLight : MonoBehaviour
{
    private PlayerController player;
    
    [SerializeField] int baseDamageDealt;
    [SerializeField] int maxDamageDealt;
    private int currentDamageDealt;
    private bool isDealingDamage = false;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        currentDamageDealt = baseDamageDealt;
    }

    void Update()
    {
        
    }

    IEnumerator RampingDamage()
    {
        while (isDealingDamage) {
            player.HealthChange(-currentDamageDealt, 0, false);
            currentDamageDealt += 1;
            yield return new WaitForSeconds(0.25f);
            if (currentDamageDealt > maxDamageDealt) {
                currentDamageDealt = maxDamageDealt;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            print("I overlapped with " + other.name);
            StartCoroutine(RampingDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            print("I stopped overlapping with " + other.name);
            StopCoroutine(RampingDamage());
            currentDamageDealt = baseDamageDealt;
        }
    }
}
