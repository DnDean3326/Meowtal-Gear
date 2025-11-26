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
        TextController();
    }

    private void TextController()
    {
        coinText.text = "Coins: " + coinCount;
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

    public int GetCoinCount()
    {
        return coinCount;
    }

    public int GetKeyCount()
    {
        print(keyCount);
        return keyCount;
    }
}
