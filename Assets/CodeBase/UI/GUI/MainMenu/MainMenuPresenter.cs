using Cysharp.Threading.Tasks;
using Data;
using Gameplay.UI;
using Infrastructure;
using Infrastructure.Services;
using System;
using UnityEngine;
using VContainer.Unity;

public class MainMenuPresenter : IStartable, IDisposable
{
    private MainMenuView _view;
    private SceneController _sceneController;
    private IAudioService _audioService;

    private AudioClip _music;

    public MainMenuPresenter(MainMenuView view, SceneController sceneController, IAudioService audioService, GameplayRegistry gameplayRegistry)
    {
        _view = view;
        _sceneController = sceneController;
        _audioService = audioService;
        _music = gameplayRegistry.SFXRegistry.MainMenuMusic;
    }

    public void Start()
    {
        _view.StartRequested += OnStartRequested;
        _view.ExitRequested += OnExitRequested;

        _audioService.PlayMusic(_music);
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
