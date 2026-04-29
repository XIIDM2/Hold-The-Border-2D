using UnityEngine;

namespace Infrastructure.Services
{
    public interface ISettingsService
    {
        string SFXSettingName { get; }
        string AmbienceSettingName { get; }
        string MusicSettingName { get; }
        string CurrentQualitySettingName { get; }

        public void DecreaseQuality();
        public void IncreaseQuality();
        public void SetSFXVolume(float value);
        public void SetAmbienceVolume(float value);
        public void SetMusicVolume(float value);
        public void Save();
    }
}