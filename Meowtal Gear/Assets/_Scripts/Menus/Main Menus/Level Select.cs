using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void StartMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LevelOneButton()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelTwoButton()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
