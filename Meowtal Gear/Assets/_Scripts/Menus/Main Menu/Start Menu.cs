using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject howToUI;
    private bool startActive;
    private bool howToActive;

    private void Start()
    {
        startUI.SetActive(true);
        howToUI.SetActive(false);
        startActive = true;
        howToActive = false;
    }
    
    private void Update()
    {
        switch (startActive) {
            case true:
                startUI.SetActive(true);
                break;
            case false:
                startUI.SetActive(false);
                break;
        }
        
        switch (howToActive) {
            case true:
                howToUI.SetActive(true);
                break;
            case false:
                howToUI.SetActive(false);
                break;
        }
    }
    
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LevelSelectButton()
    {
        SceneManager.LoadScene(3);
    }
    
    public void ControlsButton()
    {
        startActive = false;
        howToActive = true;
    }
    
    public void ResumeButton()
    {
        startActive = true;
        howToActive = false;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
