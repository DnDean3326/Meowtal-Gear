using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    private PlayerController player;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI keyText;
    private int coinCount;
    private int keyCount;
    
    void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
    }
    
    void Update()
    {
        coinText.text = "Coins: " + coinCount;
        if (coinCount >= 100) {
            coinCount -= 100;
            //player.Heal(100);
        }
    }
    
    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
    }
    
    public void AddKeys(int coinsToAdd)
    {
        coinCount += coinsToAdd;
    }

    public void SubtractKeys(int keysToSubtract)
    {
        keyCount -= keysToSubtract;
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public int GetKeyCount()
    {
        return keyCount;
    }
}
