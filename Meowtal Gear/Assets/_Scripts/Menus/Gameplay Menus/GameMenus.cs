using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject howToUI;
    private bool isPaused = false;
    private bool isLearning = true;

    void Start() 
    {
        PauseUI.SetActive(false);
        howToUI.SetActive(true);
    }

    void Update()
    {
        if (isPaused) {
            PauseUI.SetActive(true);
        }
        if (!isPaused) {
            PauseUI.SetActive(false);
        }
        
        if (isPaused || isLearning) {
            Time.timeScale = 0;
        }
        if (!isPaused && !isLearning) {
            Time.timeScale = 1;
        }
        
    }
    
    void OnPause(InputValue value)
    {
        if (value.isPressed) {
            isPaused = !isPaused;
        }
    }

    public void ResumeButton() {
        isPaused = false;
    }

    public void MenuButton() {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
    public void PlayButton() {
        isLearning = false;
        howToUI.SetActive(false);
    }
}
