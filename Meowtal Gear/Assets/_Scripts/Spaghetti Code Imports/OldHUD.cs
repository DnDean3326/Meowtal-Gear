using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Sprite[] HeartSprites;
    [SerializeField] Image HeartUI;
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    
    void Update()
    {
        if (player.currentHealth < 0) { player.currentHealth = 0; }
        {
            HeartUI.sprite = HeartSprites[player.currentHealth];
        }
    }
}
