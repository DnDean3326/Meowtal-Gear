using UnityEngine;

public class HowToText : MonoBehaviour
{
    [SerializeField] GameObject howToUI;
    private bool isLearning = true;

    void Start() 
    {
        howToUI.SetActive(true);
    }

    void Update()
    {
        if (isLearning) {
            Time.timeScale = 0;
        }
    }
    
    public bool CheckIsLearning()
    {
        return isLearning;
    }
    
    public void PlayButton() {
        isLearning = false;
        howToUI.SetActive(false);
    }
}
