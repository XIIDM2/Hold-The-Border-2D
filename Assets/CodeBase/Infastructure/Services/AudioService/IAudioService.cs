using UnityEngine;

namespace Infrastructure.Services
{
    public interface IAudioService
    {
        public void PlaySound(AudioClip clip);
        public void PlayMusic(AudioClip clip);
        public void PlayAmbience(AudioClip clip);

        public void SetSFXVolume(float volume);
        public void SetMusicVolume(float volume);
        public void SetAmbienceVolume(float volume);

    }
}