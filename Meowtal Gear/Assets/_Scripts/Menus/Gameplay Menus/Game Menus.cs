using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject howToUI;
    private bool pauseActive;
    private bool howToActive;

    private void Start()
    {
        pauseUI.SetActive(false);
        howToUI.SetActive(false);
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
                break;
            case false:
                howToUI.SetActive(false);
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
    }
}
