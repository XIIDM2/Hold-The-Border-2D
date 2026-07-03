using Infrastructure.Services;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class SettingsPresenter : IStartable, IDisposable
    {
        private readonly SettingsView _view;
        private readonly ISettingsService _settings;

        public SettingsPresenter(SettingsView view, ISettingsService settings)
        {
            _view = view;
            _settings = settings;
        }

        public void Start()
        {
            _view.DecreaseQualityRequested += OnDecreseQualityRequested;
            _view.IncreaseQualityRequested += OnIncreaseQualityRequested;

            _view.SFXVolumeChanged += OnSFXVolumeChanged;
            _view.AmbienceVolumeChanged += OnAmbienceVolumeChanged;
            _view.MusicVolumeChanged += OnMusicVolumeChanged;

            _view.ExitSettingsRequested += OnExitRequested;

            _view.Init(_settings.CurrentQualitySettingName, _settings.SFXVolumeValue, _settings.AmbienceVolume, _settings.MusicVolume);
        }

        public void Dispose()
        {
            _view.DecreaseQualityRequested -= OnDecreseQualityRequested;
            _view.IncreaseQualityRequested -= OnIncreaseQualityRequested;

            _view.SFXVolumeChanged -= OnSFXVolumeChanged;
            _view.AmbienceVolumeChanged -= OnAmbienceVolumeChanged;
            _view.MusicVolumeChanged -= OnMusicVolumeChanged;

            _view.ExitSettingsRequested -= OnExitRequested;
        }

        private void OnDecreseQualityRequested()
        {
            _settings.DecreaseQuality();
            _view.SetCurrentQualityText(_settings.CurrentQualitySettingName);
        }

        private void OnIncreaseQualityRequested()
        {
            _settings.IncreaseQuality();
            _view.SetCurrentQualityText(_settings.CurrentQualitySettingName);
        }

        private void OnSFXVolumeChanged(float value)
        {
            _settings.SetSFXVolume(value);
        }

        private void OnAmbienceVolumeChanged(float value)
        {
            _settings.SetAmbienceVolume(value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            _settings.SetMusicVolume(value);
        }

        private void OnExitRequested()
        {
            _settings.Save();
        }

    }
}