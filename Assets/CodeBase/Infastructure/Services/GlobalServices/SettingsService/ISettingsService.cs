using UnityEngine;

namespace Infrastructure.Services
{
    public interface ISettingsService
    {
        string CurrentQualitySettingName { get; }
        public float SFXVolumeValue { get; }
        public float AmbienceVolume { get; }
        public float MusicVolume { get; }

        public void Init();
        public void DecreaseQuality();
        public void IncreaseQuality();
        public void SetSFXVolume(float value);
        public void SetAmbienceVolume(float value);
        public void SetMusicVolume(float value);
        public void Save();
    }
}