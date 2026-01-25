using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
    private const int MAIN_MENU_INDEX = 0;
    public void StartTime()
    {
        Time.timeScale = 1.0f;
    }

    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(MAIN_MENU_INDEX);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
