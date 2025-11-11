using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    private PlayerController player;
    public int coinCount;
    public TextMeshProUGUI coinText;
    private bool isLearning;
    private bool paused;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    
    void Update()
    {
        coinText.text = "Coins:" + coinCount;
        if (coinCount >= 100) {
            coinCount -= 100;
            player.Heal(5);
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinText.text = "Coins:  " + coinCount;
    }
}
