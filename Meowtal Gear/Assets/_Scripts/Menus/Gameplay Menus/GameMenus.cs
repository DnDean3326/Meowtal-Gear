using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject howToUI;
    [SerializeField] private bool isPaused;
    [SerializeField] private bool isLearning;

    void Start() 
    {
        PauseUI.SetActive(false);
        howToUI.SetActive(false);
        isPaused = false;
        isLearning = false;
    }

    void Update()
    {
        switch (isPaused) {
            case true:
                PauseUI.SetActive(true);
                break;
            case false:
                PauseUI.SetActive(false);
                break;
        }
        
        /*switch (isLearning) {
            case true:
                howToUI.SetActive(true);
                break;
            case false:
                howToUI.SetActive(false);
                break;
        }*/

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

    public void ResumeButton()
    {
        print(name + " was clicked.");
        isPaused = false;
        print (name + " is paused? " + isPaused + ".");
    }

    public void LevelSelectButton()
    {
        SceneManager.LoadScene(0);
    }
    
    public void ControlsButton()
    {
        print(name + " was clicked.");
        //isPaused = false;
        isLearning = true;
        print (name + " is learning? " + isLearning + ".");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
    public void ControlsResumeButton() {
        isLearning = false;
        howToUI.SetActive(false);
    }
}
