using UnityEngine;

namespace Infrastructure.Services
{
    public class SettingsService : ISettingsService
    {
        public string CurrentQualitySettingName => QualitySettings.names[QualitySettings.GetQualityLevel()];
        public float SFXVolumeValue => _audioService.SFXVolume;
        public float AmbienceVolume => _audioService.AmbienceVolume;
        public float MusicVolume => _audioService.MusicVolume;

        private const string SFX_SETTING_NAME = "SFX";
        private const string AMBIENCE_SETTING_NAME = "Ambience";
        private const string MUSIC_SETTING_NAME = "Music";
        private const string QUALITY_SETTING_NAME = "Quality";

        private readonly IAudioService _audioService;

        public SettingsService(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public void Init()
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(QUALITY_SETTING_NAME, 2));
            _audioService.SFXVolume = PlayerPrefs.GetFloat(SFX_SETTING_NAME, 0.5f);
            _audioService.AmbienceVolume = PlayerPrefs.GetFloat(AMBIENCE_SETTING_NAME, 0.5f);
            _audioService.MusicVolume = PlayerPrefs.GetFloat(MUSIC_SETTING_NAME, 0.2f);
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
            PlayerPrefs.SetFloat(SFX_SETTING_NAME, _audioService.SFXVolume);
            PlayerPrefs.SetFloat(AMBIENCE_SETTING_NAME, _audioService.AmbienceVolume);
            PlayerPrefs.SetFloat(MUSIC_SETTING_NAME, _audioService.MusicVolume);
        }
    }
}