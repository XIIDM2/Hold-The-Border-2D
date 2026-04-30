using UnityEngine;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class SettingsService : ISettingsService
    {
        public string SFXSettingName { get; init; } = "SFX";
        public string AmbienceSettingName { get; init; } = "Ambience";
        public string MusicSettingName { get; init; } = "Music";

        private const string QUALITY_SETTING_NAME = "Quality";

        public string CurrentQualitySettingName => QualitySettings.names[QualitySettings.GetQualityLevel()];

        private readonly IAudioService _audioService;

        public SettingsService(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public void Init()
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(QUALITY_SETTING_NAME, 2));
            _audioService.SFXVolume = PlayerPrefs.GetFloat(SFXSettingName, 0.5f);
            _audioService.AmbienceVolume = PlayerPrefs.GetFloat(AmbienceSettingName, 0.5f);
            _audioService.MusicVolume = PlayerPrefs.GetFloat(MusicSettingName, 0.5f);
        }

        public void DecreaseQuality()
        {
            QualitySettings.DecreaseLevel();
        }

        public void IncreaseQuality()
        {
            QualitySettings.IncreaseLevel();
        }

        public void SetSFXVolume(float value)
        {
            _audioService.SFXVolume = value;
        }

        public void SetAmbienceVolume(float value)
        {
            _audioService.AmbienceVolume = value;
        }

        public void SetMusicVolume(float value)
        {
            _audioService.MusicVolume = value;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(QUALITY_SETTING_NAME, QualitySettings.GetQualityLevel());
            PlayerPrefs.SetFloat(SFXSettingName, _audioService.SFXVolume);
            PlayerPrefs.SetFloat(AmbienceSettingName, _audioService.AmbienceVolume);
            PlayerPrefs.SetFloat(MusicSettingName, _audioService.MusicVolume);
        }
    }
}