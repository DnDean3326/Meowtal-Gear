using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    private PlayerController player;
    public int coinCount;
    public int keyCount;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI keyText;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    
    void Update()
    {
        coinText.text = "Coins: " + coinCount;
        if (coinCount >= 100) {
            coinCount -= 100;
            player.Heal(5);
        }
        
        keyText.text = "Keys: " + keyCount;
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
    }
    
    public void AddKeys(int keysToAdd)
    {
        keyCount += keysToAdd;
    }
}
