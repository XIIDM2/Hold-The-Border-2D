using Cysharp.Threading.Tasks;
using Gameplay.UI;
using Infrastructure;
using System;
using VContainer.Unity;

public class MainMenuPresenter : IStartable, IDisposable
{
    private MainMenuView _view;
    private SceneController _sceneController;

    public MainMenuPresenter(MainMenuView view, SceneController sceneController)
    {
        _view = view;
        _sceneController = sceneController;
    }

    public void Start()
    {
        _view.StartRequested += OnStartRequested;
        _view.ExitRequested += OnExitRequested;
    }

    public void Dispose()
    {
        _view.StartRequested -= OnStartRequested;
        _view.ExitRequested -= OnExitRequested;
    }

    private void OnStartRequested()
    {
        _sceneController.ChangeScene(3).Forget();
    }

    private void OnExitRequested()
    {
        _sceneController.Exit();
    }
}
