using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject howToUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject HUD;
    private GameUIController myUIController;
    private bool pauseActive;
    private bool howToActive;

    private void Awake()
    {
        myUIController = FindFirstObjectByType<GameUIController>();
    }
    
    private void Start()
    {
        pauseUI.SetActive(false);
        howToUI.SetActive(false);
        gameUI.SetActive(true);
        HUD.SetActive(true);
        pauseActive = false;
        howToActive = false;
    }
    
    private void Update()
    {
        switch (pauseActive) {
            case true:
                pauseUI.SetActive(true);
                break;
            case false:
                pauseUI.SetActive(false);
                break;
        }
        
        switch (howToActive) {
            case true:
                howToUI.SetActive(true);
                gameUI.SetActive(false);
                HUD.SetActive(false);
                break;
            case false:
                howToUI.SetActive(false);
                //gameUI.SetActive(true);
                //HUD.SetActive(true);
                break;
        }

        if (pauseActive || howToActive) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    private void OnPause(InputValue value)
    {
        if (value.isPressed) {
            pauseActive = !pauseActive;
        }
    }

    public void ResumeButton()
    {
        pauseActive = false;
    }

    public void LevelSelectButton()
    {
        SceneManager.LoadScene(3);
    }

    public void ControlsButton()
    {
        pauseActive = false;
        howToActive = true;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void HowToResumeButton()
    {
        pauseActive = true;
        howToActive = false;
        myUIController.Link();
    }
}
