using UnityEngine;
using UnityEngine.InputSystem.XR;
using VContainer;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private SceneController _controller;

    [Inject]
    public void Construct(SceneController sceneController)
    {
        _controller = sceneController;
    }

    private void Start()
    {
        _pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        _controller.StopTime();
        _pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        _controller.StartTime();
        _pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
        _controller.StartTime();
        _controller.RestartScene();
    }

    public void ExitToMainMenu()
    {
        _controller.LoadMainMenuScene();
    }
}
